using DddGym.Framework.BaseTypes.Application.Cqrs;

namespace GymManagement.Application.Usecases.Rooms.Commands.DeleteRoom;

public sealed record DeleteRoomCommand(
    Guid GymId,
    Guid RoomId)
    : ICommand<DeleteRoomResponse>;