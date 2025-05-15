using DddGym.Framework.BaseTypes.Events;

namespace GymManagement.Domain.AggregateRoots.Sessions.Events;

public sealed record ReservationCanceledEvent(
    Session Session,
    Reservation Reservation) : IDomainEvent;