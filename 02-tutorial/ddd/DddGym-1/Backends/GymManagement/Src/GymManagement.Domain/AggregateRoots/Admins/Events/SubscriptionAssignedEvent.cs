using DddGym.Framework.BaseTypes.Events;
using GymManagement.Domain.AggregateRoots.Subscriptions;

namespace GymManagement.Domain.AggregateRoots.Admins.Events;

// SubscriptionSetEvent         : 기술적 용어
// SubscriptionAssignedEvent    : 비즈니스적 용어
public sealed record SubscriptionAssignedEvent(
    Admin Admin,
    Subscription Subscription) : IDomainEvent;