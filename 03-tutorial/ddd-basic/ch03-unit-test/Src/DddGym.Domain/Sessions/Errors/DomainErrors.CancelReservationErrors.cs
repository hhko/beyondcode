using ErrorOr;

namespace DddGym.Domain.Sessions.Errors;

public static partial class DomainErrors
{
    public static class CancelReservationErrors
    {
        public readonly static Error CannotCancelReservationTooCloseToSession = Error.Validation(
            code: $"{nameof(DomainErrors)}.{nameof(Session)}.{nameof(CannotCancelReservationTooCloseToSession)}",
            description: "Cannot cancel reservation too close to session start time");
    }
}
