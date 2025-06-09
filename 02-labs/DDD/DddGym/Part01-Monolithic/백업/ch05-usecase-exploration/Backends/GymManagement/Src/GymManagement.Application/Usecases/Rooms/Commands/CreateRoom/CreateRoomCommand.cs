using DddGym.Framework.BaseTypes.Cqrs;

namespace GymManagement.Application.Usecases.Rooms.Commands.CreateRoom;

public sealed record CreateRoomCommand(
    Guid GymId,
    string RoomName)
    : ICommand<CreateRoomResponse>;