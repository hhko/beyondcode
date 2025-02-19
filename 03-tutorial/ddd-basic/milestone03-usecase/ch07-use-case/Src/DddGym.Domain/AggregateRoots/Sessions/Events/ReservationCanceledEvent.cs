using DddGym.Domain.Abstractions.BaseTypes;

namespace DddGym.Domain.AggregateRoots.Sessions.Events;

public sealed record ReservationCanceledEvent(
    Session Session,
    Reservation Reservation) : IDomainEvent
{
    //public static readonly Error ParticipantNotFound = EventualConsistencyError.From(
    //    code: "ReservationCanceledEvent.ParticipantNotFound",
    //    description: "Participant not found");

    //public static readonly Error ParticipantScheduleUpdateFailed = EventualConsistencyError.From(
    //    code: "ReservationCanceledEvent.ParticipantScheduleUpdateFailed",
    //    description: "Removing session from participant schedule failed");
}
