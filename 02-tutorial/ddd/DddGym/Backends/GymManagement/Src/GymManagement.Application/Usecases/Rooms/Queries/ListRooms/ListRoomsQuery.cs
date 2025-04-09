using DddGym.Framework.BaseTypes.Cqrs;

namespace GymManagement.Application.Usecases.Rooms.Queries.ListRooms;

public sealed record ListRoomsQuery(
    Guid GymId)
    : IQuery2<ListRoomsResponse>;