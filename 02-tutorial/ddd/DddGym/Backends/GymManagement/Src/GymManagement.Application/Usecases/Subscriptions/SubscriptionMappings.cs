using GymManagement.Application.Usecases.Subscriptions.Commands;
using GymManagement.Application.Usecases.Subscriptions.Queries;
using GymManagement.Domain.AggregateRoots.Subscriptions;

namespace GymManagement.Application.Usecases.Subscriptions;

internal static class SubscriptionMappings
{
    public static CreateSubscriptionCommand.Response ToCreateSubscriptionResponse(
        this Subscription subscription)
    {
        return new CreateSubscriptionCommand.Response(subscription);
    }

    // To{C/R/U/D}Reponse
    public static ListSubscriptionsQuery.Response ToListSubscriptionsResponse(
        this List<Subscription> subscriptions)
    {
        return new ListSubscriptionsQuery.Response(subscriptions);
    }
}