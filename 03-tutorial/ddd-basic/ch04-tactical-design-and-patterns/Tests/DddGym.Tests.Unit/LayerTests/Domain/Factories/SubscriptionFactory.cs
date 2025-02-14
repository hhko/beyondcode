using DddGym.Domain.Subscriptions;
using DddGym.Domain.Subscriptions.Enumerations;
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