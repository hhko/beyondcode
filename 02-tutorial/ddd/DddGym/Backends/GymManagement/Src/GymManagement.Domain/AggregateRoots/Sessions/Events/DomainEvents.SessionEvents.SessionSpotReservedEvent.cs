using FunctionalDdd.Framework.BaseTypes.Events;

namespace GymManagement.Domain.AggregateRoots.Sessions.Events;

public static partial class DomainEvents
{
    public static partial class SessionEvents
    {
        public sealed record SessionSpotReservedEvent(
            Session Session,
            Reservation Reservation) : IDomainEvent;
    }
}