using DddGym.Framework.BaseTypes.Events;

namespace GymManagement.Domain.AggregateRoots.Users.Events;

public record ParticipantProfileCreatedEvent(
    Guid UserId,
    Guid ParticipantId) : IDomainEvent;