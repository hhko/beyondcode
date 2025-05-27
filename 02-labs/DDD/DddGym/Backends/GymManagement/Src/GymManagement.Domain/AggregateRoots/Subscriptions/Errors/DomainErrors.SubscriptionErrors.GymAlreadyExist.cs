using GymDdd.Framework.BaseTypes;

namespace GymManagement.Domain.AggregateRoots.Subscriptions.Errors;

public static partial class DomainErrors
{
    public static partial class SubscriptionErrors
    {
        public static Error GymAlreadyExist(Guid subscriptionId, Guid gymId) =>
            ErrorCodeFactory.Create(
               $"{nameof(DomainErrors)}.{nameof(SubscriptionErrors)}.{nameof(GymAlreadyExist)}",
               $"Subscription '{subscriptionId}' already has a gym '{gymId}'");
    }
}