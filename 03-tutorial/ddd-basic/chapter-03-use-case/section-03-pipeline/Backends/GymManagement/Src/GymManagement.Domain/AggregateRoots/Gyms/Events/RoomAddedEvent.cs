using DddGym.Framework.BaseTypes.Domain;
using GymManagement.Domain.AggregateRoots.Rooms;

namespace GymManagement.Domain.AggregateRoots.Gyms.Events;

//public sealed record RoomAddedEvent(
//    Gym Gym,
//    Room Room) : IDomainEvent;

// TODO?: 왜 Id가 아니고 객체인가? RoomDeletedEvent일 때는 Id을 전달하고 있는데???

//public record RoomAddedIntegrationEvent(
//    string Name,
//    Guid RoomId,
//    Guid GymId,
//    int MaxDailySessions) : IIntegrationEvent;

public record RoomAddedEvent(
    string Name,
    Guid RoomId,
    Guid GymId,
    int MaxDailySessions) : IDomainEvent;