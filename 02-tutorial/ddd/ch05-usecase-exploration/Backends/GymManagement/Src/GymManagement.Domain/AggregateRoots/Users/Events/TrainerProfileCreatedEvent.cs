using DddGym.Framework.BaseTypes.Events;

namespace GymManagement.Domain.AggregateRoots.Users.Events;

public record TrainerProfileCreatedEvent(
    Guid UserId,
    Guid TrainerId) : IDomainEvent;