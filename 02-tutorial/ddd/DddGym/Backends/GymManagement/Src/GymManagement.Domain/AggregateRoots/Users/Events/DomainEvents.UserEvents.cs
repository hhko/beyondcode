using DddGym.Framework.BaseTypes.Events;

namespace GymManagement.Domain.AggregateRoots.Users.Events;

public static partial class DomainEvents
{
    public static class UserEvents
    {
        public record AdminProfileCreatedEvent(
            Guid UserId,
            Guid AdminId) : IDomainEvent;

        public record ParticipantProfileCreatedEvent(
            Guid UserId,
            Guid ParticipantId) : IDomainEvent;

        public record TrainerProfileCreatedEvent(
            Guid UserId,
            Guid TrainerId) : IDomainEvent;
    }
}
