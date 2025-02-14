using DddGym.Domain.Participants;
using DddGym.Tests.Unit.LayerTests.Domain.Constants;

namespace DddGym.Tests.Unit.LayerTests.Domain.Factories;

public static class ParticipantFactory
{
    public static Participant CreateParticipant(
        Guid? userId = null,
        Guid? id = null)
    {
        return new Participant(
            userId: userId ?? DomainConstants.User.Id,
            id: id ?? DomainConstants.Participant.Id);
    }
}
