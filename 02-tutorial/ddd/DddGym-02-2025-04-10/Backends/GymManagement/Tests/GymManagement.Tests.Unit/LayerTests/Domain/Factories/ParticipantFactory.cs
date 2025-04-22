using GymManagement.Domain.AggregateRoots.Participants;
using GymManagement.Tests.Unit.LayerTests.Domain.Constants;

namespace GymManagement.Tests.Unit.LayerTests.Domain.Factories;

internal static class ParticipantFactory
{
    public static Participant CreateParticipant(
        Guid? userId = null,
        Guid? id = null)
    {
        return Participant.Create(
            userId: userId ?? DomainConstants.User.Id,
            id: id ?? DomainConstants.Participant.Id);
    }
}