using GymDdd.Framework.BaseTypes;
using LanguageExt.Common;

namespace GymManagement.Domain.AggregateRoots.Subscriptions.Errors;

public static partial class DomainErrors
{
    public static partial class SubscriptionErrors
    {
        public static Error MaxGymsExceeded(Guid subscriptionId, int maxGyms) =>
            ErrorCodeFactory.Create(
                $"{nameof(DomainErrors)}.{nameof(SubscriptionErrors)}.{nameof(MaxGymsExceeded)}",
                $"A subscription '{subscriptionId}' cannot have more gyms than the subscription allows {maxGyms}");
    }
}