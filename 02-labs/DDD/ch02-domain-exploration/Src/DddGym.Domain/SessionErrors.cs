using ErrorOr;

namespace DddGym.Domain;

public static partial class SessionErrors
{
    public static class CancelReservationErrors
    {
        public readonly static Error CannotCancelReservationTooCloseToSession = Error.Validation(
            code: $"{nameof(Session)}.{nameof(CannotCancelReservationTooCloseToSession)}",
            description: "Cannot cancel reservation too close to session start time");
    }

    public static class ReserveSpotErrors
    {
        public readonly static Error CannotHaveMoreReservationsThanParticipants = Error.Validation(
            code: $"{nameof(Session)}.{nameof(CannotHaveMoreReservationsThanParticipants)}",
            description: "Cannot have more reservations than participants");
    }
}
