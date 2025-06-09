using GymDdd.Framework.BaseTypes.Events;
using GymManagement.Domain.AggregateRoots.Gyms;

namespace GymManagement.Domain.AggregateRoots.Subscriptions.Events;

public static partial class DomainEvents
{
    public static partial class SubscriptionEvents
    {
        public sealed record GymAddedEvent(
            Subscription Subscription,
            Gym Gym) : IDomainEvent;
    }
}
