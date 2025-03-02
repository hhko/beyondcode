using GymManagement.Application.Usecases.Rooms.Commands.CreateRoom;
using GymManagement.Application.Usecases.Rooms.Commands.DeleteRoom;
using GymManagement.Application.Usecases.Rooms.Queries.GetRoom;
using GymManagement.Application.Usecases.Rooms.Queries.ListRooms;
using GymManagement.Domain.AggregateRoots.Rooms;

namespace GymManagement.Application.Usecases.Rooms;

internal static class RoomMappings
{
    public static CreateRoomResponse ToResponseCreated(this Room room)
    {
        return new CreateRoomResponse(room);
    }

    public static DeleteRoomResponse ToResponseDeleted(this ErrorOr.Deleted _)
    {
        return new DeleteRoomResponse();
    }

    public static ListRoomsResponse ToResponse(this List<Room> rooms)
    {
        return new ListRoomsResponse(rooms);
    }

    public static GetRoomResponse ToResponse(this Room room)
    {
        return new GetRoomResponse(room);
    }
}