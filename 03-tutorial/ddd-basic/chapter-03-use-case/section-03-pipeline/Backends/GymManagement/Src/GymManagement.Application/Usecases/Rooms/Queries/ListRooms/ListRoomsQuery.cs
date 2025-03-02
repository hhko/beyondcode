using DddGym.Framework.BaseTypes.Application.Cqrs;

namespace GymManagement.Application.Usecases.Rooms.Queries.ListRooms;

public sealed record ListRoomsQuery(
    Guid GymId)
    : IQuery<ListRoomsResponse>;