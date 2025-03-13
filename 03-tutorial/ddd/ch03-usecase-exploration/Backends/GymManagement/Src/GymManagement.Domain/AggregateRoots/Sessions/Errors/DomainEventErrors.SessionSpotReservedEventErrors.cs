using DddGym.Framework.BaseTypes.Events;
using ErrorOr;

namespace GymManagement.Domain.AggregateRoots.Sessions.Errors;

public static partial class DomainEventErrors
{
    public static class SessionSpotReservedEventErrors
    {
        public static readonly Error ParticipantScheduleUpdateFailed = DomainEventError.From(
            code: $"{nameof(DomainEventErrors)}.{nameof(Session)}.{nameof(ParticipantScheduleUpdateFailed)}",
            description: "Adding session to participant schedule failed");
    }
}