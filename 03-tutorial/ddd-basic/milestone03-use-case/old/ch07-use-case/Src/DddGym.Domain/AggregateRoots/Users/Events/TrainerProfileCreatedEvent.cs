using DddGym.Domain.Abstractions.BaseTypes;

namespace DddGym.Domain.AggregateRoots.Users.Events;

public record TrainerProfileCreatedEvent(
    Guid UserId,
    Guid TrainerId) : IDomainEvent;