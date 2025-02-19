using DddGym.Application.Usecases.Subscriptions.Queries.ListSubscriptions;
using DddGym.Domain.AggregateRoots.Subscriptions;
using ErrorOr;

namespace DddGym.Application.Usecases.Subscriptions.Mappings;

public static class SubscriptionMapping
{
    // To{C/R/U/D}Reponse
    public static IErrorOr<SubscriptionsResponse> ToResponse(this List<Subscription> subscriptions)
    {
        return new SubscriptionsResponse(subscriptions)
            .ToErrorOr();
    }
}
