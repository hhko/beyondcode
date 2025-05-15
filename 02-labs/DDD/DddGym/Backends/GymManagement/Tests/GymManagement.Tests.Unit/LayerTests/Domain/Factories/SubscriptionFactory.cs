using GymManagement.Domain.AggregateRoots.Subscriptions;
using GymManagement.Domain.AggregateRoots.Subscriptions.Enumerations;
using GymManagement.Tests.Unit.LayerTests.Domain.Constants;
using LanguageExt;

namespace GymManagement.Tests.Unit.LayerTests.Domain.Factories;

internal static class SubscriptionFactory
{
    public static Subscription CreateSubscription(
        Option<SubscriptionType> subscriptionType = default,
        Option<Guid> adminId = default,
        Option<Guid> id = default)
    {
        return Subscription.Create(
            subscriptionType: subscriptionType.IfNone(DomainConstants.Subscription.DefaultSubscriptionType),
            adminId.IfNone(DomainConstants.Admin.Id),
            id.IfNone(DomainConstants.Subscription.Id));
    }
}
