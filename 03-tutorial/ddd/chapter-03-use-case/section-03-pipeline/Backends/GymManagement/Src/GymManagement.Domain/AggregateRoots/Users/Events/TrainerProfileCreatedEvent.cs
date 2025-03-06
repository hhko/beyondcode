using DddGym.Framework.BaseTypes.Domain;

namespace GymManagement.Domain.AggregateRoots.Users.Events;

public record TrainerProfileCreatedEvent(
    Guid UserId,
    Guid TrainerId) : IDomainEvent;