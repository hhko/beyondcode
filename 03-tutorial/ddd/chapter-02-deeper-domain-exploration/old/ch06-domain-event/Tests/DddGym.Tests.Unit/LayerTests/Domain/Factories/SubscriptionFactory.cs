using DddGym.Domain.AggregateRoots.Subscriptions;
using DddGym.Domain.AggregateRoots.Subscriptions.Enumerations;
using DddGym.Tests.Unit.LayerTests.Domain.Constants;

namespace DddGym.Tests.Unit.LayerTests.Domain.Factories;

internal static class SubscriptionFactory
{
    public static Subscription CreateSubscription(
        SubscriptionType? subscriptionType = null,
        Guid? adminId = null,
        Guid? id = null)
    {
        return new Subscription(
            subscriptionType: subscriptionType ?? DomainConstants.Subscription.DefaultSubscriptionType,
            adminId ?? DomainConstants.Admin.Id,
            id ?? DomainConstants.Subscription.Id);
    }
}