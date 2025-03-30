using ErrorOr;

namespace DddGym.Domain;

public static partial class GymErrors
{
    public static class AddRoomErrors
    {
        public static readonly Error CannotHaveMoreRoomsThanSubscriptionAllows = Error.Validation(
            code: $"{nameof(Gym)}.{nameof(CannotHaveMoreRoomsThanSubscriptionAllows)}",
            description: "A gym cannot have more rooms than the subscription allows");
    }
}