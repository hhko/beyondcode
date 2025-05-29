using GymDdd.Framework.BaseTypes.Errors;

namespace GymManagement.Domain.AggregateRoots.Sessions.Errors;
public static partial class DomainErrors
{
    public static partial class SessionErrors
    {
        public static Error ReservationInPast(Guid sessionId, DateTime reservationDateTime, DateTime utcNow) =>
            ErrorCodeFactory.Create(
                $"{nameof(DomainErrors)}.{nameof(SessionErrors)}.{nameof(ReservationInPast)}",
                $"A participant cannot cancel the reservation '{reservationDateTime}' for a session '{sessionId}' that has completed '{utcNow}'");
    }
}
