using DddGym.Framework.BaseTypes.Cqrs;
using GymManagement.Domain.AggregateRoots.Rooms;

namespace GymManagement.Application.Usecases.Rooms.Commands.CreateRoom;

internal sealed record CreateRoomResponse(
    Room room)
    : IResponse;