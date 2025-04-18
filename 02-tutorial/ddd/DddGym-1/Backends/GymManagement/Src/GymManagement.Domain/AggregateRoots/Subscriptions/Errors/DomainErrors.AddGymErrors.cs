

using LanguageExt.Common;

namespace GymManagement.Domain.AggregateRoots.Subscriptions.Errors;

public static partial class DomainErrors
{
    public static class AddGymErrors
    {
        // TODO: 현재 값. 기대 값
        //public static readonly Error CannotHaveMoreGymsThanSubscriptionAllows = Error.Validation(
        //    code: $"{nameof(DomainErrors)}.{nameof(Subscription)}.{nameof(CannotHaveMoreGymsThanSubscriptionAllows)}",
        //    description: "A subscription cannot have more gyms than the subscription allows");

        public static readonly Error CannotHaveMoreGymsThanSubscriptionAllows = Error.New(
            "A subscription cannot have more gyms than the subscription allows");
    }
}