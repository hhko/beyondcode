using GymDdd.Framework.BaseTypes.Events;

namespace GymManagement.Domain.AggregateRoots.Gyms.Events;
public static partial class DomainEvents
{
    public static partial class GymEvents
    {
        public sealed record RoomRemovedEvent(
            Gym Gym,
            Guid RoomId) : IDomainEvent;
    }
}
