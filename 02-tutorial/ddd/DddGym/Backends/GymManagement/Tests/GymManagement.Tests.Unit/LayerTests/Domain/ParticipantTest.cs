using GymManagement.Domain.AggregateRoots.Participants;
using GymManagement.Domain.AggregateRoots.Sessions;
using GymManagement.Tests.Unit.LayerTests.Domain.Constants;
using GymManagement.Tests.Unit.LayerTests.Domain.Factories;
using static GymManagement.Domain.AggregateRoots.Participants.Errors.DomainErrors;
using static GymManagement.Tests.Unit.Abstractions.Constants.AssemblyConstants;

namespace GymManagement.Tests.Unit.LayerTests.Domain;

[Trait(nameof(UnitTest), UnitTest.Domain)]
public sealed class ParticipantTest
{
    // 규칙
    //  참가자는 겹치는 세션을 예약할 수 없다.
    //  A participant cannot reserve overlapping sessions
    [Theory]
    [InlineData(1, 3, 1, 3)]
    [InlineData(1, 3, 2, 3)]
    [InlineData(1, 3, 2, 4)]
    [InlineData(1, 3, 0, 2)]
    [InlineData(1, 3, 0, 4)]
    public void AddToSchedule_WhenSessionOverlapsWithAnotherSession_ShouldFail(
        int startHourSession1,
        int endHourSession1,
        int startHourSession2,
        int endHourSession2)
    {
        // Arrange
        Participant sut = ParticipantFactory.CreateParticipant();

        Session session1 = SessionFactory.CreateSession(
            date: DomainConstants.Session.Date,
            time: TimeRangeFactory.CreateFromHours(startHourSession1, endHourSession1),
            id: Guid.NewGuid());

        Session session2 = SessionFactory.CreateSession(
            date: DomainConstants.Session.Date,
            time: TimeRangeFactory.CreateFromHours(startHourSession2, endHourSession2),
            id: Guid.NewGuid());

        // Act
        var addSession1Result = sut.AddToSchedule(session1);
        var addSession2Result = sut.AddToSchedule(session2);

        // Assert
        addSession1Result.IsError.ShouldBeFalse();

        addSession2Result.IsError.ShouldBeTrue();
        addSession2Result.FirstError.ShouldBe(AddToScheduleErrors.CannotHaveTwoOrMoreOverlappingSessions);
    }
}