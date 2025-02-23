using DddGym.Domain.Abstractions.BaseTypes;

namespace DddGym.Domain.AggregateRoots.Users.Events;

public record ParticipantProfileCreatedEvent(
    Guid UserId,
    Guid ParticipantId) : IDomainEvent;
