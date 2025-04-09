using DddGym.Framework.BaseTypes.Events;
using LanguageExt.Common;


namespace GymManagement.Domain.AggregateRoots.Sessions.Errors;

public static partial class DomainEventErrors
{
    public static class ReservationCanceledEventErrors
    {
        //public static readonly Error ParticipantNotFound = DomainEventError.From(
        //    code: $"{nameof(DomainEventErrors)}.{nameof(Session)}.{nameof(ParticipantNotFound)}",
        //    description: "Participant not found");

        //public static readonly Error ParticipantScheduleUpdateFailed = DomainEventError.From(
        //    code: $"{nameof(DomainEventErrors)}.{nameof(Session)}.{nameof(ParticipantScheduleUpdateFailed)}",
        //    description: "Removing the session from the participant's schedule failed");

        public static readonly Error ParticipantNotFound = DomainEventError.From(
            "Participant not found");

        public static readonly Error ParticipantScheduleUpdateFailed = DomainEventError.From(
            "Removing the session from the participant's schedule failed");
    }
}