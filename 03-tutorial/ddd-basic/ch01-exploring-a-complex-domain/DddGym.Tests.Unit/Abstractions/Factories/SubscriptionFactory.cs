using DddGym.Domain.Subscriptions;
using DddGym.Domain.Subscriptions.Enumerations;
using DddGym.Tests.Unit.Abstractions.Constants;

namespace DddGym.Tests.Unit.Abstractions.Factories;

internal static class SubscriptionFactory
{
    public static Subscription CreateSubscription(
        Grade? subscriptionType = null,
        Guid? adminId = null,
        Guid? id = null)
    {
        return new Subscription(
            grade: subscriptionType ?? DomainConstants.Subscription.DefaultSubscriptionType,
            adminId ?? DomainConstants.Admin.Id,
            id ?? DomainConstants.Subscription.Id);
    }
}