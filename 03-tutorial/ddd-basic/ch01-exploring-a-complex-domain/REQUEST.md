- 에러 문구에 "in" 추가
  ```
  return Error.Conflict(description: "Gym already exists");  --> in Su...
  ```
- 복수형(Subscriptions)을 단수형(Subscription)으로
  ```
  Constants.Subscription.cs
        class Subscriptions
  public const int MaxDailySessions = Subscriptions.MaxDailySessionsFreeTier;
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