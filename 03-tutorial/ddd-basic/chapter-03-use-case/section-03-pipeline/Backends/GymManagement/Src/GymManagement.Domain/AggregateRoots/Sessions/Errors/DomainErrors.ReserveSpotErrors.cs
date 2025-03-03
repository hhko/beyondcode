using ErrorOr;

namespace GymManagement.Domain.AggregateRoots.Sessions.Errors;

public static partial class DomainErrors
{
    public static class ReserveSpotErrors
    {
        public static readonly Error CannotHaveMoreReservationsThanParticipants = Error.Validation(
            code: $"{nameof(Domain)}.{nameof(Session)}.{nameof(CannotHaveMoreReservationsThanParticipants)}",
            description: "Cannot have more reservations than participants");
    }
}