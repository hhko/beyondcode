using FunctionalDdd.Framework.BaseTypes.Cqrs;
using GymManagement.Domain.AggregateRoots.Rooms;

namespace GymManagement.Application.Usecases.Rooms.Queries.GetRoom;

public sealed record GetRoomResponse(
    Room romm)
    : IResponse;