using DddGym.Framework.BaseTypes.Application.Cqrs;
using GymManagement.Domain.AggregateRoots.Rooms;

namespace GymManagement.Application.Usecases.Rooms.Queries.ListRooms;

public sealed record ListRoomsResponse(
    List<Room> Rooms)
    : IResponse;