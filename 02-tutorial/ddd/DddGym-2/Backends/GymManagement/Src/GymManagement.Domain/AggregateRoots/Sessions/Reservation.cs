using DddGym.Framework.BaseTypes;

namespace GymManagement.Domain.AggregateRoots.Sessions;

public sealed class Reservation : Entity
{
    public Guid ParticipantId { get; }

    private Reservation(
        Guid participantId,
        Guid? id) : base(id ?? Guid.NewGuid())
    {
        ParticipantId = participantId;
    }

    private Reservation()
    {
    }

    public static Reservation Create(
        Guid participantId,
        Guid? id = null)
    {
        return new Reservation(participantId, id);
    }
}