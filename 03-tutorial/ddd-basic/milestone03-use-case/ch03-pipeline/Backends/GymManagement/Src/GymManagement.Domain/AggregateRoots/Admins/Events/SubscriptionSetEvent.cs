using DddGym.Framework.BaseTypes.Domain;
using GymManagement.Domain.AggregateRoots.Subscriptions;

namespace GymManagement.Domain.AggregateRoots.Admins.Events;

public sealed record SubscriptionSetEvent(
    Admin Admin,
    Subscription Subscription) : IDomainEvent;