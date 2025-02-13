using DddGym.Domain.Sessions;
using DddGym.Domain.Trainers;
using DddGym.Tests.Unit.LayerTests.Domain.Constants;
using DddGym.Tests.Unit.LayerTests.Domain.Factories;
using Shouldly;
using static DddGym.Domain.Trainers.Errors.DomainErrors;
using static DddGym.Tests.Unit.Abstractions.Constants.Constants;

namespace DddGym.Tests.Unit.LayerTests.Domain;

[Trait(nameof(UnitTest), UnitTest.Domain)]
public class TrainerTest
{
    [Theory]
    [InlineData(1, 3, 1, 3)]
    [InlineData(1, 3, 2, 3)]
    [InlineData(1, 3, 2, 4)]
    [InlineData(1, 3, 0, 2)]
    [InlineData(1, 3, 0, 4)]
    public void AddSessionToSchedule_WhenSessionOverlapsWithAnotherSession_ShouldFail(
        int startHourSession1,
        int endHourSession1,
        int startHourSession2,
        int endHourSession2)
    {
        // Arrange
        Trainer sut = TrainerFactory.CreateTrainer();

        Session session1 = SessionFactory.CreateSession(
            date: DomainConstants.Session.Date,
            time: TimeRangeFactory.CreateFromHours(startHourSession1, endHourSession1),
            id: Guid.NewGuid());

        Session session2 = SessionFactory.CreateSession(
            date: DomainConstants.Session.Date,
            time: TimeRangeFactory.CreateFromHours(startHourSession2, endHourSession2),
            id: Guid.NewGuid());

        // Act
        var addSession1Result = sut.AddSessionToSchedule(session1);
        var addSession2Result = sut.AddSessionToSchedule(session2);

        // Assert
        addSession1Result.IsError.ShouldBeFalse();

        addSession2Result.IsError.ShouldBeTrue();
        addSession2Result.FirstError.ShouldBe(AddSessionToScheduleErrors.CannotHaveTwoOrMoreOverlappingSessions);
    }
}
