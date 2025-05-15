using DddGym.Framework.BaseTypes.Events;
using GymManagement.Domain.AggregateRoots.Subscriptions;

namespace GymManagement.Domain.AggregateRoots.Admins.Events;

public sealed record SubscriptionSetEvent(
    Admin Admin,
    Subscription Subscription) : IDomainEvent;