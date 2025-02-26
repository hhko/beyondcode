using DddGym.Domain.Abstractions.BaseTypes;
using DddGym.Domain.AggregateRoots.Subscriptions;

namespace DddGym.Domain.AggregateRoots.Admins.Events;

public sealed record SubscriptionSetEvent(
    Admin Admin,
    Subscription Subscription) : IDomainEvent;