using ErrorOr;

namespace DddGym.Domain.AggregateRoots.Sessions.Errors;

public static partial class DomainErrors
{
    public static class CancelReservationErrors
    {
        public static readonly Error CannotCancelReservationTooCloseToSession = Error.Validation(
            code: $"{nameof(Domain)}.{nameof(Session)}.{nameof(CannotCancelReservationTooCloseToSession)}",
            description: "Cannot cancel reservation too close to session start time");

        public static readonly Error CannotCancelPastSession = Error.Validation(
            code: $"{nameof(Domain)}.{nameof(Session)}.{nameof(CannotCancelPastSession)}",
            description: "A participant cannot cancel a reservation for a session that has completed");
    }
}