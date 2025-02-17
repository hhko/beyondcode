using ErrorOr;

namespace DddGym.Domain.AggregateRoots.Sessions.Errors;

public static partial class DomainErrors
{
    public static class CancelReservationErrors
    {
        public static readonly Error CannotCancelReservationTooCloseToSession = Error.Validation(
            code: $"{nameof(DomainErrors)}.{nameof(Session)}.{nameof(CannotCancelReservationTooCloseToSession)}",
            description: "Cannot cancel reservation too close to session start time");
    }
}