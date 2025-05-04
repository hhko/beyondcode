## Functional DDD 리팩토링

### 도메인 지식 코드화하기
- 도메인 레이어와 애플리케이션 레이어의 코드는 기술적인 관점의 용어보다, 비즈니스 도메인의 의도를 드러내는 용어로 클래스 이름과 메서드 이름을 구성해야 합니다.
- 이를 통해 코드 자체가 도메인 지식을 표현하는 모델이 되며, 코드를 읽는 것만으로도 비즈니스 규칙과 흐름을 이해할 수 있어야 합니다.

### Map과 Bind 함수 이해하기
```cs
// Monad 스타일
.Map(_ => Pure(x))
.Bind(y => SideEffect(y));

// Monad LINQ 스타일
from x in SizeEffect(y)
let y = Pure()
select unit;
```

### 합성 함수 만들기
- 함수를 작게 나누어 마치 레고 블록처럼 연결해서 처리 흐름을 만듭니다.

```cs
// 적용 전 1. Imperative Guard 스타일
public Fin<Guid> PromoteToTrainer()
{
  if (TrainerId is not null)
      return UserErrors.TrainerAlreadyPromoted(TrainerId.Value);

  Guid newTrainerId = Guid.NewGuid();

  TrainerId = newTrainerId;
  _domainEvents.Add(new TrainerPromotedEvent(Id, newTrainerId));

  return TrainerId.Value;
}

// 적용 후 2. Monad 스타일
public Fin<Guid> PromoteToTrainer()
{
  return EnsureTrainerNotPromoted(TrainerId)
    .Map(_ => NewTrainerId())
    .Bind(newTrainerId => ApplyTrainerPromotion(newTrainerId));

  // 로컬 함수: EnsureTrainerNotPromoted, ...
}

// 적용 후 3. Monad LINQ 스타일
public Fin<Guid> PromoteToTrainer()
{
  // 합성 함수
  return from _1 in EnsureTrainerNotPromoted(TrainerId)
         let newTrainerId = NewTrainerId()
         from _2 in ApplyTrainerPromotion(newTrainerId)
         select newTrainerId;

  // 로컬 함수
  Fin<Unit> EnsureTrainerNotPromoted(Guid? trainerId) =>
    trainerId.HasValue
      ? UserErrors.TrainerAlreadyPromoted(trainerId.Value)
      : unit;

  Guid NewTrainerId() =>
    Guid.NewGuid();

  Fin<Guid> ApplyTrainerPromotion(Guid newTrainerId)
  {
    TrainerId = newTrainerId;
    _domainEvents.Add(new TrainerPromotedEvent(Id, newTrainerId));
    return newTrainerId;
  }
}
```

### void 반환 메서드 개선하기
```cs
// 적용 전
void UnregisterSession(Guid sessionId)

// 적용 후
Fin<Unit> UnregisterSession(Guid sessionId)
```

- void를 반환하는 메서드는 값을 전달하지 않기 때문에, 이후에 함수 체인으로 연결할 수 없습니다.
- 따라서 체인 구성을 가능하게 하기 위해 반환 타입을 Unit으로 변경합니다.
- 또한 void를 반환하는 메서드는 부수 효과(예: 상태 변경, 외부 시스템 호출 등)를 포함하고 있기 때문에, 반환 타입을 `Fin<Unit>`으로 변경합니다.

```cs
// 적용 전. Imperative Guard 스타일
public Fin<Unit> UnscheduleSession(Session session)
{
  if (!_sessionIds.Contains(session.Id))
  {
      return TrainerErrors.SessionNotScheduled;
  }

  var unbookTimeSlotResult = _schedule.UnbookTimeSlot(session.Date, session.Time);
  if (unbookTimeSlotResult.IsFail)
  {
      return (Error)unbookTimeSlotResult;
  }

  _sessionIds.Remove(session.Id);

  return unit;
}

// 적용 후. Monad LINQ 스타일
public Fin<Unit> UnscheduleSession(Session session)
{
  return from _1 in EnsureSessionScheduled(session.Id)
         from _2 in _schedule.UnbookTimeSlot(session.Date, session.Time)
         from _3 in UnregisterSession(session.Id)
         select unit;

  Fin<Unit> EnsureSessionScheduled(Guid sessionId) =>
    _sessionIds.Contains(sessionId)
      ? unit
      : TrainerErrors.SessionNotScheduled(sessionId);

  // void -> Fin<Unit>
  Fin<Unit> UnregisterSession(Guid sessionId)
  {
    _sessionIds.Remove(sessionId);    // 부수 효과
    return unit;
  }
}
```

### 조기 반환 메서드 개선하기
```cs
// 적용 전. Imperative Guard 스타일
internal Fin<Unit> BookTimeSlot(DateOnly date, TimeRange newTimeSlot)
{
  if (!_calendar.TryGetValue(date, out var timeSlots))
  {
      _calendar[date] = [time];
      return unit;      // 조기 반환
  }

  if (timeSlots.Any(timeSlot => timeSlot.OverlapsWith(time)))
  {
      return ScheduleErrors.CannotHaveTwoOrMoreOverlappingSessions(date, time);
  }

  timeSlots.Add(time);
  return unit;
}

// 적용 후. Monad LINQ 스타일
internal Fin<Unit> BookTimeSlot(DateOnly date, TimeRange newTimeSlot)
{
  return from timeSlots in GetOrCreateTimeSlots(date)
         from _1 in CheckOverlap(date, timeSlots, newTimeSlot)
         from _2 in ApplyTimeSlotToCalendar(timeSlots, newTimeSlot)
         select unit;

  // 조기 반환을 Get Or Create 동작으로 개선합니다.
  Fin<List<TimeRange>> GetOrCreateTimeSlots(DateOnly date)
  {
    if (!_calendar.TryGetValue(date, out var slots))
    {
      slots = new List<TimeRange>();
      _calendar[date] = slots;
    }

    return slots;
  }

  Fin<Unit> CheckOverlap(DateOnly date, List<TimeRange> timeSlots, TimeRange newTimeSlot) =>
    timeSlots.Any(existingTimeSlot => timeSlot.OverlapsWith(newTimeSlot))
      ? ScheduleErrors.CannotHaveTwoOrMoreOverlappingSessions(date, newTimeSlot)
      : unit;

  Fin<Unit> ApplyTimeSlotToCalendar(List<TimeRange> timeSlots, TimeRange newTimeSlot)
  {
    timeSlots.Add(newTimeSlot);
    return unit;
  }
}
```