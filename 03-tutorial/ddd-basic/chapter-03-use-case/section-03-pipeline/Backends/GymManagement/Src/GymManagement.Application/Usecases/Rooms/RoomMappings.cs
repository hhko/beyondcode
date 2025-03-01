using GymManagement.Application.Usecases.Rooms.Commands.CreateRoom;
using GymManagement.Application.Usecases.Rooms.Commands.DeleteRoom;
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
}