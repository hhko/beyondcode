using DddGym.Domain.Abstractions.BaseTypes;

namespace DddGym.Domain.AggregateRoots.Sessions.Events;

public sealed record SessionCanceledEvent(Session Session) : IDomainEvent
{
    //public static readonly Error TrainerNotFound = EventualConsistencyError.From(
    //    code: "SessionCanceledEvent.TrainerNotFound",
    //    description: "Trainer not found");

    //public static readonly Error TrainerScheduleUpdateFailed = EventualConsistencyError.From(
    //    code: "SessionCanceledEvent.TrainerScheduleUpdateFailed",
    //    description: "Removing session from trainer's schedule failed");

    //public static readonly Error ParticipantScheduleUpdateFailed = EventualConsistencyError.From(
    //    code: "SessionCanceledEvent.ParticipantScheduleUpdateFailed",
    //    description: "Removing session from participant schedule failed");
}
