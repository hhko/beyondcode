using GymManagement.Application.Usecases.Rooms.Commands;
using GymManagement.Application.Usecases.Rooms.Queries;
using GymManagement.Domain.AggregateRoots.Rooms;

namespace GymManagement.Application.Usecases.Rooms;

internal static class RoomMappings
{
    public static CreateRoomCommand.Response ToCreateRoomResponse(this Room room)
    {
        return new CreateRoomCommand.Response(room);
    }

    //public static DeleteRoomResponse ToResponseDeleted(this ErrorOr.Deleted _)
    //{
    //    return new DeleteRoomResponse();
    //}

    public static ListRoomsQuery.Response ToListRoomsResponse(this List<Room> rooms)
    {
        return new ListRoomsQuery.Response(rooms);
    }

    public static GetRoomQuery.Response ToGetRoomResponse(this Room room)
    {
        return new GetRoomQuery.Response(room);
    }
}