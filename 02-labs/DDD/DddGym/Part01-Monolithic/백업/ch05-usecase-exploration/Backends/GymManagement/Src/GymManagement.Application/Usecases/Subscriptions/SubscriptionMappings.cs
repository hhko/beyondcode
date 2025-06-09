using GymManagement.Application.Usecases.Subscriptions.Commands.CreateSubscription;
using GymManagement.Application.Usecases.Subscriptions.Queries.ListSubscriptions;
using GymManagement.Domain.AggregateRoots.Subscriptions;

namespace GymManagement.Application.Usecases.Subscriptions;

internal static class SubscriptionMappings
{
    public static CreateSubscriptionResponse ToResponse(
        this Subscription subscription)
    {
        return new CreateSubscriptionResponse(subscription);
    }

    // To{C/R/U/D}Reponse
    public static ListSubscriptionsResponse ToResponse(
        this List<Subscription> subscriptions)
    {
        return new ListSubscriptionsResponse(subscriptions);
    }
}