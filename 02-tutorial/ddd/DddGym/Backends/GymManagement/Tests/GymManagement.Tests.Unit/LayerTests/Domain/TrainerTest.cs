using DddGym.Framework.BaseTypes;
using GymManagement.Domain.AggregateRoots.Sessions;
using GymManagement.Domain.AggregateRoots.Trainers;
using GymManagement.Tests.Unit.LayerTests.Domain.Factories;
using LanguageExt.Common;
using static GymManagement.Tests.Unit.Abstractions.Constants.Constants;
using GymManagement.Domain.SharedTypes.Errors;
using static GymManagement.Domain.SharedTypes.Errors.DomainErrors;
using GymManagement.Domain.SharedTypes.ValueObjects;
using LanguageExt;
using LanguageExt.Traits;

namespace GymManagement.Tests.Unit.LayerTests.Domain;

[Trait(nameof(UnitTest), UnitTest.Domain)]
public sealed class TrainerTest
{
    // TODO: LanguageExt
    //// 규칙
    ////  트레이너는 두 개 이상의 겹치는 세션을 가르칠 수 없다.
    ////  A trainer cannot teach two or more overlapping sessions
    //[Theory]
    //[InlineData(1, 3, 1, 3)]
    //[InlineData(1, 3, 2, 3)]
    //[InlineData(1, 3, 2, 4)]
    //[InlineData(1, 3, 0, 2)]
    //[InlineData(1, 3, 0, 4)]
    //public void AddSessionToSchedule_WhenSessionOverlapsWithAnotherSession_ShouldFail(
    //    int startHourSession1,
    //    int endHourSession1,
    //    int startHourSession2,
    //    int endHourSession2)
    //{
    //    // Arrange
    //    Trainer sut = TrainerFactory.CreateTrainer();

    //    Session session1 = SessionFactory.CreateSession(
    //        date: DomainConstants.Session.Date,
    //        time: TimeRangeFactory.CreateFromHours(startHourSession1, endHourSession1),
    //        id: Guid.NewGuid());

    //    Session session2 = SessionFactory.CreateSession(
    //        date: DomainConstants.Session.Date,
    //        time: TimeRangeFactory.CreateFromHours(startHourSession2, endHourSession2),
    //        id: Guid.NewGuid());

    //    // Act
    //    var addSession1Result = sut.AddSessionToSchedule(session1);
    //    var addSession2Result = sut.AddSessionToSchedule(session2);

    //    // Assert
    //    addSession1Result.IsError.ShouldBeFalse();

    //    addSession2Result.IsError.ShouldBeTrue();
    //    addSession2Result.FirstError.ShouldBe(AddSessionToScheduleErrors.CannotHaveTwoOrMoreOverlappingSessions);
    //}


    // 트레이너는 존재하지 않는 날짜의 세션을 해제할 수 없습니다.
    // A trainer cannot unschedule sessions if the date does not exist.
    [Fact]
    public void Trainer_Cannot_UnscheduleSession_When_DateNotFound()
    {
        // Arrange
        Trainer sut = TrainerFactory.CreateTrainer();
        Session session = SessionFactory.CreateSession();
        Session wrongSession = SessionFactory.CreateSession(
            id: session.Id,
            date: session.Date.AddDays(1));     // 다른 날짜 Session

        var addSessionToScheduleResult = sut.ScheduleSession(session);

        // Act: 다른 날짜의 세션은 삭제할 수 없습니다.
        var actual = sut.UnscheduleSession(wrongSession);

        // Assert
        actual.IsFail.ShouldBeTrue();

        actual.ShouldBeErrorCode($"{nameof(DomainErrors)}.{nameof(ScheduleErrors)}.{nameof(ScheduleErrors.DateNotFound)}");
    }

    // 트레이너는 존재하지 않는 날짜의 세션을 해제할 수 없습니다.
    // A trainer cannot unschedule sessions if the date does not exist.
    [Fact]
    public void Trainer_Cannot_UnscheduleSession_When_TimeSlotNotFound()
    {
        // Arrange
        Trainer sut = TrainerFactory.CreateTrainer();
        Session session = SessionFactory.CreateSession();
        Session wrongSession = SessionFactory.CreateSession(
            id: session.Id,
            timeSlot: (TimeSlot)TimeSlot.Create(        // 다른 TimeSlot Session
                session.TimeSlot.Start,
                session.TimeSlot.End.AddHours(3))); 

        var addSessionToScheduleResult = sut.ScheduleSession(session);

        // Act: 다른 날짜의 세션은 삭제할 수 없습니다.
        var actual = sut.UnscheduleSession(wrongSession);

        // Assert
        actual.IsFail.ShouldBeTrue();
        actual.ShouldBeErrorCode($"{nameof(DomainErrors)}.{nameof(ScheduleErrors)}.{nameof(ScheduleErrors.TimeSlotNotFound)}");
    }
}



//internal static class Common
//{
//    internal static void ThrowIfSome<T>(T _)
//        => throw new Exception("Expected None, got Some instead.");

//    internal static void ThrowIfNone()
//        => throw new Exception("Expected Some, got None instead.");

//    internal static void ThrowIfFail<T>(T _)
//        => throw new Exception("Expected Success, got Fail instead.");

//    internal static void ThrowIfSuccess<T>(T _)
//        => throw new Exception("Expected Fail, got Success instead.");

//    internal static void ThrowIfRight<T>(T _)
//        => throw new Exception("Expected Left, got Right instead.");

//    internal static void ThrowIfLeft<T>(T _)
//        => throw new Exception("Expected Right, got Left instead.");

//    internal static void ThrowExpectedFailGotSome<T>(T _)
//        => throw new Exception("Expected Fail, got Some instead.");

//    internal static void ThrowExpectedFailGotNone()
//        => throw new Exception("Expected Fail, got None instead.");

//    internal static void SuccessfulNone()
//    {
//        /* we should end up in here*/
//    }

//    internal static void Noop<T>(T _) { }
//}