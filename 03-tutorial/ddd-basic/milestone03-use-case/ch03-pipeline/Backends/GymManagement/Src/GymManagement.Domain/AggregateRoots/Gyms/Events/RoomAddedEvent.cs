using DddGym.Framework.BaseTypes.Domain;
using GymManagement.Domain.AggregateRoots.Rooms;

namespace GymManagement.Domain.AggregateRoots.Gyms.Events;

public sealed record RoomAddedEvent(
    Gym gym,
    Room room) : IDomainEvent;
