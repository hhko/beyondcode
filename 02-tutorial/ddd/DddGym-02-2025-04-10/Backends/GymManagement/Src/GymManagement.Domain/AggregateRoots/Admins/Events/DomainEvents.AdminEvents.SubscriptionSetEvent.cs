using DddGym.Framework.BaseTypes.Events;
using GymManagement.Domain.AggregateRoots.Subscriptions;

namespace GymManagement.Domain.AggregateRoots.Admins.Events;

// SubscriptionSetEvent         : 기술적 용어
// SubscriptionAssignedEvent    : 비즈니스적 용어
//public sealed record SubscriptionSetEvent(
//    Admin Admin,
//    Subscription Subscription) : IDomainEvent;

public static partial class DomainEvents
{
    public static partial class AdminEvents
    {
        public sealed record SubscriptionSetEvent(
            Admin Admin,
            Subscription Subscription) : IDomainEvent;
    }
}