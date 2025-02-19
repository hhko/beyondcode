using DddGym.Domain.Abstractions.BaseTypes;

namespace DddGym.Domain.AggregateRoots.Users.Events;

public record AdminProfileCreatedEvent(
    Guid UserId,
    Guid AdminId) : IDomainEvent;
