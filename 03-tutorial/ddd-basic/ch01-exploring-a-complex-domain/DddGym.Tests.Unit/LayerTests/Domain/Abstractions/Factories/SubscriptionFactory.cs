using DddGym.Domain.Subscriptions;
using DddGym.Domain.Subscriptions.Enumerations;

namespace DddGym.Tests.Unit.LayerTests.Domain.Abstractions.Factories;

internal static class SubscriptionFactory
{
    public static Subscription CreateSubscription(
        Grade? subscriptionType = null,
        Guid? adminId = null,
        Guid? id = null)
    {
        return new Subscription(
            subscriptionType: subscriptionType ?? Constants.Subscription.DefaultSubscriptionType,
            adminId ?? Constants.Admin.Id,
            id ?? Constants.Subscription.Id);
    }
}
