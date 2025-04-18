using DddGym.Framework.BaseTypes;
using LanguageExt.Common;

namespace GymManagement.Domain.AggregateRoots.Subscriptions.Errors;

public static partial class DomainErrors
{
    public static class SubscriptionErrors
    {
        public static Error GymAlreadyExist(Guid subscriptionId, Guid gymId) =>
            ErrorCodeFactory.New(
               $"{nameof(DomainErrors)}.{nameof(SubscriptionErrors)}.{nameof(GymAlreadyExist)}",
               $"Subscription '{subscriptionId}' already has a gym '{gymId}'");

        public static Error MaxGymsExceeded(Guid subscriptionId, int maxGyms) =>
            ErrorCodeFactory.New(
                $"{nameof(DomainErrors)}.{nameof(SubscriptionErrors)}.{nameof(MaxGymsExceeded)}",
                $"A subscription '{subscriptionId}' cannot have more gyms than the subscription allows {maxGyms}");
    }
}