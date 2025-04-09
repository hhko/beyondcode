using DddGym.Framework.BaseTypes.Events;

namespace GymManagement.Domain.AggregateRoots.Users.Events;

public record AdminProfileCreatedEvent(
    Guid UserId,
    Guid AdminId) : IDomainEvent;