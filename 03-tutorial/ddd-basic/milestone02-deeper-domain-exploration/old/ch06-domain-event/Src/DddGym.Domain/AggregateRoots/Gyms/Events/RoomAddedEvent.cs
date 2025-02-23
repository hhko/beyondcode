using DddGym.Domain.Abstractions.BaseTypes;
using DddGym.Domain.AggregateRoots.Rooms;

namespace DddGym.Domain.AggregateRoots.Gyms.Events;

public sealed record RoomAddedEvent(
    Gym gym,
    Room room) : IDomainEvent;
