using FunctionalDdd.Framework.BaseTypes;
using LanguageExt.Common;

namespace GymManagement.Domain.AggregateRoots.Sessions.Errors;
public static partial class DomainErrors
{
    public static partial class SessionErrors
    {
        public static Error ReservationTooClose(Guid sessionId, DateTime reservationDateTime, DateTime utcNow) =>
            ErrorCodeFactory.Create(
                $"{nameof(DomainErrors)}.{nameof(SessionErrors)}.{nameof(ReservationTooClose)}",
                $"A participant Cannot cancel the reservation '{reservationDateTime}' too close to session '{sessionId}' start time '{utcNow}'");
    }
}
