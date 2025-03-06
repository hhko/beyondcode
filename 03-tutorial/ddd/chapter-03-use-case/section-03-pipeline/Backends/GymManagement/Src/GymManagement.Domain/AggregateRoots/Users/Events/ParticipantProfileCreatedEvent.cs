using DddGym.Framework.BaseTypes.Domain;

namespace GymManagement.Domain.AggregateRoots.Users.Events;

public record ParticipantProfileCreatedEvent(
    Guid UserId,
    Guid ParticipantId) : IDomainEvent;