using DddGym.Framework.BaseTypes.Cqrs;

namespace GymManagement.Application.Usecases.Rooms.Queries.GetRoom;

public sealed record GetRoomQuery(
    Guid GymId,
    Guid RoomId)
    : IQuery<GetRoomResponse>;