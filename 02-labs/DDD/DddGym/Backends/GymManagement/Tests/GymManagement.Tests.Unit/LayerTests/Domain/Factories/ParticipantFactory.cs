using GymManagement.Domain.AggregateRoots.Participants;
using GymManagement.Tests.Unit.LayerTests.Domain.Constants;

namespace GymManagement.Tests.Unit.LayerTests.Domain.Factories;

internal static class ParticipantFactory
{
    public static Participant CreateParticipant(
        Option<Guid> userId = default,
        Option<Guid> id = default)
    {
        return Participant.Create(
            userId: userId.IfNone(DomainConstants.User.Id),
            id: id.IfNone(DomainConstants.Participant.Id));
    }
}