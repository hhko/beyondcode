using DddGym.Framework.BaseTypes.Domain;

namespace GymManagement.Domain.AggregateRoots.Users.Events;

public record AdminProfileCreatedEvent(
    Guid UserId,
    Guid AdminId) : IDomainEvent;
