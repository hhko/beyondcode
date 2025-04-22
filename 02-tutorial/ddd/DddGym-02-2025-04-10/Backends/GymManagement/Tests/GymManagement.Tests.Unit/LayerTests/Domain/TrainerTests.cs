using DddGym.Framework.BaseTypes;
using GymManagement.Domain.AggregateRoots.Sessions;
using GymManagement.Domain.AggregateRoots.Trainers;
using GymManagement.Domain.SharedTypes.Errors;
using GymManagement.Domain.SharedTypes.ValueObjects;
using GymManagement.Tests.Unit.Abstractions;
using GymManagement.Tests.Unit.LayerTests.Domain.Constants;
using GymManagement.Tests.Unit.LayerTests.Domain.Factories;
using LanguageExt;
using LanguageExt.Traits;
using static GymManagement.Domain.AggregateRoots.Trainers.Errors.DomainErrors;
using static GymManagement.Domain.SharedTypes.Errors.DomainErrors;
using static GymManagement.Tests.Unit.Abstractions.Constants.Constants;

namespace GymManagement.Tests.Unit.LayerTests.Domain;

[Trait(nameof(UnitTest), UnitTest.Domain)]
public sealed class TrainerTests
{
    // 규칙
    //  트레이너는 두 개 이상의 겹치는 세션을 가르칠 수 없다.
    //  A trainer cannot teach two or more overlapping sessions
    [Theory]
    [InlineData(1, 3, 1, 3)]     // ! ~ 3, 1 ~ 3
    [InlineData(1, 3, 2, 3)]     // 1 ~ 3, 2 ~ 3
    [InlineData(1, 3, 2, 4)]     // 1 ~ 3, 2 ~ 4
    [InlineData(1, 3, 0, 2)]     // 1 ~ 3, 0 ~ 2
    [InlineData(1, 3, 0, 4)]     // 1 ~ 3, 0 ~ 4
    public void Trainer_Cannot_ScheduleSession_When_TwoOrMoreOverlappingSessions(
        int startHourSession1,
        int endHourSession1,
        int startHourSession2,
        int endHourSession2)
    {
        // Arrange
        Trainer sut = TrainerFactory.CreateTrainer();

        Session session1 = SessionFactory.CreateSession(
            date: DomainConstants.Session.Date,
            timeSlot: TimeRangeFactory.CreateFromHours(startHourSession1, endHourSession1),
            id: Guid.NewGuid());

        Session session2 = SessionFactory.CreateSession(
            date: DomainConstants.Session.Date,
            timeSlot: TimeRangeFactory.CreateFromHours(startHourSession2, endHourSession2),
            id: Guid.NewGuid());

        // Act
        var actualSession1 = sut.ScheduleSession(session1);
        var actualSession2 = sut.ScheduleSession(session2);

        // Assert
        actualSession1.IsSucc.ShouldBeTrue();
        actualSession2.IsFail.ShouldBeTrue();
        actualSession2.ShouldBeErrorCodes(
            ErrorCodeFactory.Format(
                nameof(DomainErrors),
                nameof(TrainerErrors),
                nameof(TrainerErrors.SessionNotScheduled)),
            ErrorCodeFactory.Format(
                nameof(DomainErrors),
                nameof(ScheduleErrors),
                nameof(ScheduleErrors.TimeSlotOverlapped)));
    }


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
        actual.ShouldBeErrorCodes(
            ErrorCodeFactory.Format(
                nameof(DomainErrors),
                nameof(TrainerErrors),
                nameof(TrainerErrors.SessionNotFound)),
            ErrorCodeFactory.Format(
                nameof(DomainErrors),
                nameof(ScheduleErrors),
                nameof(ScheduleErrors.DateNotFound)));
    }

    // 트레이너는 존재하지 않는 시간의 세션을 해제할 수 없습니다.
    // A trainer cannot unschedule sessions if the time slot does not exist.
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
        actual.ShouldBeErrorCodes(
            ErrorCodeFactory.Format(
                nameof(DomainErrors),
                nameof(TrainerErrors),
                nameof(TrainerErrors.SessionNotFound)),
            ErrorCodeFactory.Format(
                nameof(DomainErrors),
                nameof(ScheduleErrors),
                nameof(ScheduleErrors.TimeSlotNotFound)));
    }
}
