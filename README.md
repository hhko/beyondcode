[![Build VitePress](https://github.com/hhko/beyondcode/actions/workflows/build-vitepress.yml/badge.svg)](https://github.com/hhko/beyondcode/actions/workflows/build-vitepress.yml)
[![Build C# Template](https://github.com/hhko/beyondcode/actions/workflows/build-template.yml/badge.svg)](https://github.com/hhko/beyondcode/actions/workflows/build-template.yml)
[![Build C# Gym](https://github.com/hhko/beyondcode/actions/workflows/build-gym.yml/badge.svg)](https://github.com/hhko/beyondcode/actions/workflows/build-gym.yml)

> Make It Work, Make It Right, Make It Fast

## 목표
> 코드는 팀이 함께 완성해 가는 한 편의 글입니다. 우리는 그 글을 차곡차곡 쌓아 시스템을 만들어 갑니다.
- 소스 코드의 구조는 **책의 목차처럼** 명확하고 직관적이어야 하며, 이를 통해 도메인과 시스템을 자연스럽게 이해할 수 있어야 합니다.
- 테스트 코드는 검증 도구를 넘어, **비즈니스 규칙을 이해하고 학습하는** 데 핵심적인 가이드 역할을 해야 합니다.

<br/>

## 도메인 주도 설계와 함수형 프로그래밍

### 주요 개념
도메인 주도 설계의 '무엇을 표현할지'와 함수형 프로그래밍의 '어떻게 표현할지'가 만나서, 변경에 강하고, 테스트 가능하고, 명확한 의도를 가진 코드를 만듭니다.
- **무엇을 표현할지: 복잡성 분리**
  - 복잡한 비즈니스 로직을 도메인 모델 중심으로 풀어나가는 설계 방법입니다.
  - 도메인 전문가의 언어 (Ubiquitous Language) 로 시스템을 설계하는 것이 핵심입니다.
- **어떻게 표현할지: 부작용 최소화**
  - 함수(수학적인 함수)에 기반한 프로그래밍 방식입니다.
  - 상태 변경 없이, 입력에 따라 일관된 출력을 보장합니다.

### 공통 목표
- **변경에 강한 모델**
  - DDD: 복잡성 분리 (관심사의 분리: 도메인과 기술)
  - FP: 부작용 최소화 (합성 함수: 부작용 없는 순수 함수 연결)
- **예측 가능한 동작**
  - DDD: 명확한 경계 (Bounded Context)
  - FP: 순수 함수 지향
- **정확한 도메인 표현**
  - DDD: 명확한 의미 부여 (Ubiquitous Language)
  - FP: 타입 기반 설계

<br/>

## Application Architecture

### Architecture Technical Map
![](./.images/ArchitectureTechMap.png)

### Internal Architecture (Hexagonal Architecture)
![hexagonal architecture](./01-architecture/part1-overview/ch03-internal-architecture/.images/Architecture.Internal.Hexagonal.png)

### External Architecture
> TODO

<br/>

## Hands-on Labs
'[Getting Started: Domain-Driven Design](https://dometrain.com/course/getting-started-domain-driven-design-ddd/?ref=dometrain-github&promo=getting-started-domain-driven-design)' 강의 예제를 Functional 도메인 주도 설계와 Functional 아키텍처로 재구성합니다.

### 목차
- Part 1. 도메인
  - [ ] Chapter 01. 도메인 탐험
  - [ ] Chapter 02. 도메인 구조화
  - [ ] Chapter 03. 도메인 함수화
  - [ ] Chapter 04. 도메인 단위 테스트
- Part 2. 유스케이스
  - [ ] Chapter 05. 유스케이스 탐험 (CQRS)
  - [ ] Chapter 06. 유스케이스 파이프라인
  - [ ] Chapter 07. 유스케이스 단위 테스트
  - [ ] Chapter 08. 유스케이스 시나리오 테스트 (Cucumber)
- Part 3. Monolithic
  - [ ] Chapter 09. WebApi
  - [ ] Chapter 10. 유스케이스 통합 테스트
  - [ ] Chapter 11. OpenTelemetry
  - [ ] Chapter 12. PostgreSQL
  - [ ] Chapter 13. Cache
  - [ ] Chapter 14. Containerization
- Part 4. Microservices
  - [ ] Chapter 15. Aspire
  - [ ] Chapter 16. RabbitMQ
  - [ ] Chapter 17. Resilience
  - [ ] Chapter 18. Reverse Proxy
  - [ ] Chapter 19. Chaos Engineering
- Part 5. 운영
  - [ ] Chapter 20. OpenFeature (Feature Flag Management)
  - [ ] Chapter 21. OpenSearch (Observability System)
  - [ ] Chapter 22. Ansible (Infrastructure as Code)
  - [ ] Chapter 23. Backstage (Building developer portals)

### 소스 폴더 구성 원칙

- **관심사의 분리: 비즈니스 관심사 vs. 기술 관심사**
  - 관심사의 분리는 레이어로 구분됩니다.
    - Adapter 레이어: 기술 관심사
    - Application 레이어: 비즈니스 관심사 (도메인 흐름)
    - Domain 레이어: 비즈니스 관심사 (도메인 단위)
- **목표의 분리: 주요 목표 vs. 부수 목표 (주된 목표에 따르는 부수적인 목표)**
  - 목표의 분리는 배치 방향으로 구분됩니다.
    - 위쪽: 기술적인 측면에서 더 중요한 것(부수 목표: Abstractions)을 배치합니다.
    - 아래쪽: 비즈니스 측면에서 더 중요한 것(주요 목표)을 배치합니다.

| 방향    | 관심사의 분리           | 목표의 분리                                 |
| ---    | ---                             | ---                               |
| 위쪽    | 기술 관심사 (무한)       | 부수 목표 (무한 -**_Abstractions_**-> 유한)  |
| 아래쪽  | 비즈니스 관심사 (유한)    | 주요 목표 (유한)                            |

- 레이어의 주요 목표를 직관적으로 이해하기 위해, 여러 부수 목표를 Abstractions 폴더 아래에 배치하여 부수 목표를 하나로 묶습니다.
- 이렇게 하면 부수 목표는 Abstractions 폴더 안에 고정되어 상단에 위치하게 되어,
- 주요 목표와 쉽게 구분할 수 있어 주요 목표를 더 잘 이해할 수 있습니다.

```shell
{T}
 ├─Src
 │  ├─{T}                          // Host               > 위쪽: 기술 관심사 (부수 목표)
 │  ├─{T}.Adapters.Infrastructure  // Adapter Layer      >  │
 │  ├─{T}.Adapters.Persistence     // Adapter Layer      >  │
 │  ├─{T}.Application              // Application Layer  >  ↓
 │  └─{T}.Domain                   // Domain Layer       > 아래쪽: 비즈니스 관심사 (주요 목표)
 │     │
 │     ├─Abstractions                                    > 위쪽: 기술 관심사 (부수 목표)
 │     │                                                 >  ↓
 │     └─AggregateRoots                                  > 아래쪽: 비즈니스 관심사 (주요 목표)
 │
 └─Tests
    ├─{T}.Tests.Integration        // Integration Test   > 위쪽: 기술 관심사 (부수 목표)
    ├─{T}.Tests.Performance        // Performance Test   >  ↓
    └─{T}.Tests.Unit               // Unit Test          > 아래쪽: 비즈니스 관심사 (주요 목표)
```

<br/>

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
