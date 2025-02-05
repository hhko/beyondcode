using ErrorOr;

namespace DddGym.Domain.Subscriptions.Errors;

public static partial class DomainErrors
{
    public static class SubscriptionError
    {
        // TODO: 현재 값. 기대 값
        public static readonly Error CannotHaveMoreGymsThanSubscriptionAllows = Error.Validation(
            $"{nameof(Subscription)}.{nameof(CannotHaveMoreGymsThanSubscriptionAllows)}",
            "A subscription cannot have more gyms than the subscription allows");
    }
}
