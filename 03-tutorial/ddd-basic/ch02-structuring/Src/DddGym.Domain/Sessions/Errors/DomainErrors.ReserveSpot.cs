using ErrorOr;

namespace DddGym.Domain.Sessions.Errors;

public static partial class DomainErrors
{
    public static class ReserveSpotErrors
    {
        public readonly static Error CannotHaveMoreReservationsThanParticipants = Error.Validation(
            code: $"{nameof(DomainErrors)}.{nameof(Session)}.{nameof(CannotHaveMoreReservationsThanParticipants)}",
            description: "Cannot have more reservations than participants");
    }
}
