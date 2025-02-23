using DddGym.Domain.Abstractions.BaseTypes;

namespace DddGym.Domain.AggregateRoots.Sessions;

// 추가
public sealed class Reservation : Entity
{
    public Guid ParticipantId { get; }

    public Reservation(
        Guid participantId,
        Guid? id = null) : base(id ?? Guid.NewGuid())
    {
        ParticipantId = participantId;
    }

    // TODO: 존재 이유 ???
    private Reservation()
    {
    }
}