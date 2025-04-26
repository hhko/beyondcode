using FunctionalDdd.Framework.BaseTypes.Cqrs;

namespace GymManagement.Application.Usecases.Rooms.Commands.DeleteRoom;

public sealed record DeleteRoomCommand(
    Guid GymId,
    Guid RoomId)
    : ICommand2<DeleteRoomResponse>;