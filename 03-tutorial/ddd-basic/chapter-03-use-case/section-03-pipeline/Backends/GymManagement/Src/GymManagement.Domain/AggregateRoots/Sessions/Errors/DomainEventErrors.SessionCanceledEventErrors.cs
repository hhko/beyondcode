using DddGym.Framework.BaseTypes.Application.Events;
using ErrorOr;
using GymManagement.Domain.Abstractions.Entities;

namespace GymManagement.Domain.AggregateRoots.Sessions.Errors;

public static partial class DomainEventErrors
{
    public static class SessionCanceledEventErrors
    {
        //public static readonly Error TrainerNotFound = EventualConsistencyError.From(
        //    code: "SessionCanceledEvent.TrainerNotFound",
        //    description: "Trainer not found");

        //public static readonly Error TrainerScheduleUpdateFailed = EventualConsistencyError.From(
        //    code: "SessionCanceledEvent.TrainerScheduleUpdateFailed",
        //    description: "Removing session from trainer's schedule failed");

        public static readonly Error ParticipantScheduleUpdateFailed = DomainEventError.From(
            code: $"{nameof(DomainEventErrors)}.{nameof(Session)}.{nameof(ParticipantScheduleUpdateFailed)}",
            description: "Removing session from participant schedule failed");
    }
}