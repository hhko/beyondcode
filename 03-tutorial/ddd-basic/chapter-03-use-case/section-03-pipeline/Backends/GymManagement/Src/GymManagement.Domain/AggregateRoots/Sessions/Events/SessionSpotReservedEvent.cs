using DddGym.Framework.BaseTypes.Domain;

namespace GymManagement.Domain.AggregateRoots.Sessions.Events;

public sealed record SessionSpotReservedEvent(
    Session Session,
    Reservation Reservation) : IDomainEvent;