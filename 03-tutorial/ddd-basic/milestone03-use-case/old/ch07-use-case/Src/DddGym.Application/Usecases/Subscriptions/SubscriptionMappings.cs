using DddGym.Application.Usecases.Subscriptions.Commands.CreateSubscription;
using DddGym.Application.Usecases.Subscriptions.Queries.ListSubscriptions;
using DddGym.Domain.AggregateRoots.Subscriptions;

namespace DddGym.Application.Usecases.Subscriptions;

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
