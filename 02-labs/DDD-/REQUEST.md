- 에러 문구에 "in" 추가
  ```
  return Error.Conflict(description: "Gym already exists");  --> in Su...
  ```
- 복수형(Subscriptions)을 단수형(Subscription)으로
  ```
  Constants.Subscription.cs
        class Subscriptions
  public const int MaxDailySessions = Subscriptions.MaxDailySessionsFreeTier;

  Constants.Participants.cs
        clsss Participant
  ```
- 접근 제어
  ```
  public sealed class
  internal sealed class
  ```
- 누락된 테스트 추가
  ```cs
  [Theory]
  [InlineData(1, 3, 1, 3)]
  [InlineData(1, 3, 2, 3)]
  [InlineData(1, 3, 2, 4)]
  [InlineData(1, 3, 0, 2)]
  [InlineData(1, 3, 0, 4)]
  public void ScheduleSession_WhenSessionOverlapsWithAnotherSession_ShouldFail(
      int startHourSession1,
      int endHourSession1,
      int startHourSession2,
      int endHourSession2)
  {
      // Arrange
      Room sut = RoomFactory.CreateRoom(1 <-- 확인 필요 --->);

      Session session1 = SessionFactory.CreateSession(
          date: DomainConstants.Session.Date,
          time: TimeRangeFactory.CreateFromHours(startHourSession1, endHourSession1),
          id: Guid.NewGuid());

      Session session2 = SessionFactory.CreateSession(
          date: DomainConstants.Session.Date,
          time: TimeRangeFactory.CreateFromHours(startHourSession2, endHourSession2),
          id: Guid.NewGuid());

      // Act
      var addSession1Result = sut.ScheduleSession(session1);
      var addSession2Result = sut.ScheduleSession(session2);

      // Assert
      addSession1Result.IsError.ShouldBeFalse();

      addSession2Result.IsError.ShouldBeTrue();
      addSession2Result.FirstError.ShouldBe(ScheduleSessionErrors.CannotHaveTwoOrMoreOverlappingSessions);
  }
  ```
- if 구문 통일
  ```
  // 변경 전
  if (bookTimeSlotResult.IsError && bookTimeSlotResult.FirstError.Type == ErrorType.Conflict)
  {
      return AddSessionToScheduleErrors.CannotHaveTwoOrMoreOverlappingSessions;
  }

  // addEventResult

  // 변경 후
  if (bookTimeSlotResult.IsError)
  {
      return bookTimeSlotResult.FirstError.Type == ErrorType.Conflict
          ? AddToSchedule.CannotHaveTwoOrMoreOverlappingSessions
          : bookTimeSlotResult.Errors;
  }

  // bookTimeSlotResult
  ```
- 함수 매개변수 통일
  ```
  // 無
  // 有
    code: $"{nameof(DomainErrors)}.{nameof(Gym)}.{nameof(CannotHaveMoreRoomsThanSubscriptionAllows)}",
    description:
  ```
- 초과 테스트 개선
  ```cs
  [Fact]
  public void ScheduleSession_WhenMoreThanSubscriptionAllows_ShouldFail()
  {
      // Arrange
      int maxDailySessions = 1;
      Room sut = RoomFactory.CreateRoom(maxDailySessions: maxDailySessions);

      var sessions = Enumerable.Range(0, maxDailySessions + 1)
          .Select(_ => SessionFactory.CreateSession(id: Guid.NewGuid()))
          .ToList();

      // Act
      List<ErrorOr<Success>> scheduleSessionResults = sessions.ConvertAll(sut.ScheduleSession);

      // Assert: 추가 성공 검증(마지막 추가를 제외한 결과)
      IEnumerable<ErrorOr<Success>> allButLastScheduleSession = scheduleSessionResults.Take(..^1);
      allButLastScheduleSession.ShouldAllBe(result => !result.IsError);

      // Assert: 추가 실패 검증(마지막 추가 결과)
      ErrorOr<Success> lastScheduleSessionResult = scheduleSessionResults[scheduleSessionResults.Count - 1];
      lastScheduleSessionResult.IsError.ShouldBeTrue();
      lastScheduleSessionResult.FirstError.ShouldBe(ScheduleSessionErrors.CannotHaveMoreSessionThanSubscriptionAllows);
  }
  ```
- session 5. rooms
  ```cs
  RemoveFromSchedule
    //return Error.Conflict("Trainer already assigned to teach session");
    return Error.NotFound(description: "session not found");
  ```
- 용어 통일???
  - TimeRange
  - TimeSlot
  - Time
- 에러 전파 방법???
  ```cs
  if (removeBookingResult.IsError)
  {
    // ?
    return removeBookingResult.Errors;

    // ?
    return removeBookingResult;
  }
  ```
- 사전 정의된 에러 검사 방법
  ```
  ShouldBe(Error.NotFound);       // 에러
  ```
- IDateTimeProvider -> .NET
  - TimeProvider
  - Microsoft.Extensions.TimeProvider.Testing
  - https://grantwinney.com/how-to-use-timeprovider-and-faketimeprovider-to-test-timers/
  - https://code-maze.com/csharp-testing-time-dependent-code-with-timeprovider/
  - https://andrewlock.net/exploring-the-dotnet-8-preview-avoiding-flaky-tests-with-timeprovider-and-itimer/?ref=grantwinney.com
  ---
  - https://learn.microsoft.com/en-us/dotnet/standard/datetime/timeprovider-overview

- Participant 생성자
  ```cs
  public Participant(
    Schedule? schedule = null,
  ```
- 이벤트에서 왜 객체를 전달하나? id가 아니라???
  ```
  _domainEvents.Add(new RoomRemovedEvent(this, room));

  room.Id???
  ```
- 배열 객체에 추가한 후 DB에 업데이트하는 방법은?
  ```
  // CreateGymCommand

  // 현재
  await _subscriptionsRepository.UpdateAsync(subscription);

  // 개선???
  commit???
  ```
- domain factory
  ```
  CreateGymCommandUsecase

  var gym = new Gym(
      name: command.Name,
      maxRooms: subscription.GetMaxRooms(),
      subscriptionId: subscription.Id);
  ```
- 변수 이름 변경
  ```
  // public async Task<ErrorOr<Room>> Handle(CreateRoomCommand command, CancellationToken cancellationToken)
  // 전
  var addGymResult = gym.AddRoom(room);

  // 후
  var addRoomResult = gym.AddRoom(room);
  ```
- 메시지 문법 오류
  ```
  // 수정 전
  have reservation

  // 수정 후
  have a reservation
  ```
- Throw 도입
  ```
  var participant = await _participantsRepository.GetByIdAsync(notification.Reservation.ParticipantId)
            ?? throw new EventualConsistencyException(ReservationCanceledEvent.ParticipantNotFound);

  var participant = ...
  participant.ThrowIfNull(_ => new EventualConsistencyException(Error.NotFound(description: "Participant not found")));
  ```
- Error 타입 변경
  ```
  var removeBookingResult = participant.RemoveFromSchedule(notification.Session);

  if (removeBookingResult.IsError)
  {
      throw new EventualConsistencyException(
          ReservationCanceledEvent.ParticipantNotFound,     <-- SessionCanceledEvent.ParticipantScheduleUpdateFailed,
          removeBookingResult.Errors);
  }
  ```
- 로그 내용 개선
  ```
  // 전
  Removing session from participant schedule failed

  // 후
  Cannot Remove session from participant schedule
  ```
- ErrorOr과 Throw 통합?
- 불필요한 변수 제거
  ```cs
  public record GetRoomQuery(
    Guid GymId,               // <- 사용하지 않는 변수
    Guid RoomId) : IRequest<ErrorOr<Room>>;
  ```
- Login과 Register 위치 변경: UserManagement, Authentication
  - public class RegisterCommandHandler
- async 접두사
  ```
  // 전
  Task<List<Session>> ListByRoomId(Guid roomId);

  // 후
  Task<List<Session>> ListByRoomIdAsync(Guid roomId);
  ```

```
public enum ErrorType
{
    Failure,            // 1
    Unexpected,         // 2
    Validation,         // 3
    Conflict,           // 4
    NotFound,           // 5
    Unauthorized,       // 6
    Forbidden           // 7
}

    Event               // 100
```

- 로그 ??? 기본 형식
- The "Designing with types" series
  - https://fsharpforfunandprofit.com/series/designing-with-types/
- 조영호
  - https://github.com/eternity-oop/Woowahan-OO-01-object-reference
  - https://github.com/eternity-oop/Woowahan-OO-02-object-reference
  - https://github.com/eternity-oop/Woowahan-OO-03-object-reference
- https://mj950425.github.io/jvm-lang/dev/seminar/experience/nextstep-ddd-seminar-1/
- 도메인 표현 패턴
  - Association
  - Value Object
  - Entity
  - Service
  - Module
- 생명 주기 패턴
  - Aggregate Root
  - Repository
  - Factory