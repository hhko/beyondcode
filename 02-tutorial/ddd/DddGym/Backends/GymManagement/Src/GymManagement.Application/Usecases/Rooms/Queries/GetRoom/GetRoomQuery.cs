using FunctionalDdd.Framework.BaseTypes.Cqrs;

namespace GymManagement.Application.Usecases.Rooms.Queries.GetRoom;

public sealed record GetRoomQuery(
    Guid GymId,
    Guid RoomId)
    : IQuery2<GetRoomResponse>;