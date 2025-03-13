using DddGym.Framework.BaseTypes.Events;

namespace GymManagement.Domain.AggregateRoots.Gyms.Events;

public sealed record RoomRemovedEvent(
    Gym Gym,
    Guid RoomId) : IDomainEvent;