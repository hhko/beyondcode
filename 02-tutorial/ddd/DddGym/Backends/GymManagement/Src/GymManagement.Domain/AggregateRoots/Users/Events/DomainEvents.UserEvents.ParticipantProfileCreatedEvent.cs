using FunctionalDdd.Framework.BaseTypes.Events;

namespace GymManagement.Domain.AggregateRoots.Users.Events;

public static partial class DomainEvents
{
    public static partial class UserEvents
    {
        public record ParticipantProfileCreatedEvent(
            Guid UserId,
            Guid ParticipantId) : IDomainEvent;
    }
}
