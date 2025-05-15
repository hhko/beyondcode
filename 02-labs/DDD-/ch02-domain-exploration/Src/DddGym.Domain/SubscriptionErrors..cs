using ErrorOr;

namespace DddGym.Domain;

public static partial class SubscriptionErrors
{
    public static class AddGymErrors
    {
        // TODO: 현재 값. 기대 값
        public static readonly Error CannotHaveMoreGymsThanSubscriptionAllows = Error.Validation(
            code: $"{nameof(Subscription)}.{nameof(CannotHaveMoreGymsThanSubscriptionAllows)}",
            description: "A subscription cannot have more gyms than the subscription allows");
    }
}