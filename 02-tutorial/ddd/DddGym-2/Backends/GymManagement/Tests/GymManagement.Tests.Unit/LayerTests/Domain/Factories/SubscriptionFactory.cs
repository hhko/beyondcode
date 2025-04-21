using GymManagement.Domain.AggregateRoots.Subscriptions;
using GymManagement.Domain.AggregateRoots.Subscriptions.Enumerations;
using GymManagement.Tests.Unit.LayerTests.Domain.Constants;

namespace GymManagement.Tests.Unit.LayerTests.Domain.Factories;

internal static class SubscriptionFactory
{
    public static Subscription CreateSubscription(
        SubscriptionType? subscriptionType = null,
        Guid? adminId = null,
        Guid? id = null)
    {
        return Subscription.Create(
            subscriptionType: subscriptionType ?? DomainConstants.Subscription.DefaultSubscriptionType,
            adminId ?? DomainConstants.Admin.Id,
            id ?? DomainConstants.Subscription.Id);
    }
}