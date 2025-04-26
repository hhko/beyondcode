using FunctionalDdd.Framework.BaseTypes.Cqrs;

namespace GymManagement.Application.Usecases.Rooms.Commands.CreateRoom;

public sealed record CreateRoomCommand(
    Guid GymId,
    string RoomName)
    : ICommand2<CreateRoomResponse>;