using GymDdd.Framework.BaseTypes;
using LanguageExt;

namespace GymManagement.Domain.AggregateRoots.Sessions;

public sealed class Reservation : Entity
{
    public Guid ParticipantId { get; }

    private Reservation(
        Guid participantId,
        Option<Guid> id) : base(id.IfNone(Guid.NewGuid()))
    {
        ParticipantId = participantId;
    }

    private Reservation()
    {
    }

    public static Reservation Create(
        Guid participantId,
        Option<Guid> id = default)
    {
        return new Reservation(participantId, id);
    }
}