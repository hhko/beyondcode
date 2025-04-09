using GymManagement.Domain.AggregateRoots.Sessions;
using GymManagement.Domain.AggregateRoots.Trainers;
using GymManagement.Tests.Unit.LayerTests.Domain.Constants;
using GymManagement.Tests.Unit.LayerTests.Domain.Factories;
using static GymManagement.Domain.AggregateRoots.Trainers.Errors.DomainErrors;
using static GymManagement.Tests.Unit.Abstractions.Constants.Constants;

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

    //[Fact]
    //public void RemoveFromSchedule_When_ShouldFail()
    //{
    //    // Arrange
    //    Trainer sut = TrainerFactory.CreateTrainer();
    //    Session session = SessionFactory.CreateSession();
    //    Session wrongSession = SessionFactory.CreateSession(
    //        id: session.Id,
    //        date: session.Date.AddDays(1));

    //    // Act
    //    var addSessionToScheduleResult = sut.AddSessionToSchedule(session);

    //    var removeFromScheduleResult = sut.RemoveFromSchedule(wrongSession);
    //    removeFromScheduleResult.IsError.ShouldBeTrue();
    //    //removeFromScheduleResult.FirstError.ShouldBe(Error.NotFound);
    //}
}