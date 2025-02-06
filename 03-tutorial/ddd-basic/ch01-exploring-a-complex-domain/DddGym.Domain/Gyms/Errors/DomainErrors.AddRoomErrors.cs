using ErrorOr;

namespace DddGym.Domain.Gyms.Errors;

public static partial class DomainErrors
{
    public static class AddRoomErrors
    {
        public static readonly Error CannotHaveMoreRoomsThanSubscriptionAllows = Error.Validation(
            $"{nameof(DomainErrors)}.{nameof(Gym)}.{nameof(CannotHaveMoreRoomsThanSubscriptionAllows)}",
            "A gym cannot have more rooms than the subscription allows");
    }
}