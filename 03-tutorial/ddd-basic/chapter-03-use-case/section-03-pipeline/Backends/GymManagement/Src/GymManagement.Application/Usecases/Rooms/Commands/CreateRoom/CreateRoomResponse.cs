using DddGym.Framework.BaseTypes.Application.Cqrs;
using GymManagement.Domain.AggregateRoots.Rooms;

namespace GymManagement.Application.Usecases.Rooms.Commands.CreateRoom;

internal sealed record CreateRoomResponse(
    Room room)
    : IResponse;
