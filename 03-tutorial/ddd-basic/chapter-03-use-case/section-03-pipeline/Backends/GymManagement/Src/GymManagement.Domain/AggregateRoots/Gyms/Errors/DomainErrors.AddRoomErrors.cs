using ErrorOr;

namespace GymManagement.Domain.AggregateRoots.Gyms.Errors;

public static partial class DomainErrors
{
    public static class AddRoomErrors
    {
        public static readonly Error CannotHaveMoreRoomsThanSubscriptionAllows = Error.Validation(
            code: $"{nameof(DomainErrors)}.{nameof(Gym)}.{nameof(CannotHaveMoreRoomsThanSubscriptionAllows)}",
            description: "A gym cannot have more rooms than the subscription allows");
    }
}