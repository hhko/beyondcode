using ErrorOr;

namespace GymManagement.Domain.AggregateRoots.Sessions.Errors;

public static partial class DomainErrors
{
    public static class CancelReservationErrors
    {
        public static readonly Error ReservationNotFound = Error.Validation(
            code: $"{nameof(DomainErrors)}.{nameof(Session)}.{nameof(ReservationNotFound)}",
            description: "Session reservation not found");

        public static readonly Error CannotCancelReservationTooCloseToSession = Error.Validation(
            code: $"{nameof(DomainErrors)}.{nameof(Session)}.{nameof(CannotCancelReservationTooCloseToSession)}",
            description: "Cannot cancel the reservation too close to session start time");

        public static readonly Error CannotCancelPastSession = Error.Validation(
            code: $"{nameof(DomainErrors)}.{nameof(Session)}.{nameof(CannotCancelPastSession)}",
            description: "A participant cannot cancel a reservation for a session that has completed");
    }
}


// Reservation cannot be canceled for a completed session
// Cannot cancel reservation: session already completed