using DddGym.Framework.BaseTypes.Cqrs;
using GymManagement.Domain.AggregateRoots.Rooms;

namespace GymManagement.Application.Usecases.Rooms.Queries.ListRooms;

public sealed record ListRoomsResponse(
    List<Room> Rooms)
    : IResponse;