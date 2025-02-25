using DddGym.Framework.BaseTypes.Domain;

namespace GymManagement.Domain.AggregateRoots.Sessions.Events;

public sealed record SessionSpotReservedEvent(
    Session Session,
    Reservation Reservation) : IDomainEvent
{
    //public static readonly Error ParticipantScheduleUpdateFailed = EventualConsistencyError.From(
    //    code: "SessionSpotReserved.ParticipantScheduleUpdateFailed",
    //    description: "Adding session to participant schedule failed");
}
