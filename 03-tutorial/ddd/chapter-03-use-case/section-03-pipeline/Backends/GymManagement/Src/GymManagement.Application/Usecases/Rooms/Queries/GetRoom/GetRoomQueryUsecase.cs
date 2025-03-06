using DddGym.Framework.BaseTypes.Application.Cqrs;
using ErrorOr;
using GymManagement.Application.Abstractions.Repositories;
using GymManagement.Domain.AggregateRoots.Rooms;

namespace GymManagement.Application.Usecases.Rooms.Queries.GetRoom;

internal sealed class GetRoomQueryUsecase
    : IQueryUsecase<GetRoomQuery, GetRoomResponse>
{
    private readonly IRoomsRepository _roomsRepository;

    public GetRoomQueryUsecase(IRoomsRepository roomsRepository)
    {
        _roomsRepository = roomsRepository;
    }

    public async Task<IErrorOr<GetRoomResponse>> Handle(GetRoomQuery query, CancellationToken cancellationToken)
    {
        Room? room = await _roomsRepository.GetByIdAsync(query.RoomId);
        if (room is null)
        {
            return Error
                .NotFound(description: "Room not found")
                .ToErrorOr<GetRoomResponse>();
        }

        // TODO: Response 없이 바로 room 전달 
        return room
            .ToResponse()
            .ToErrorOr();
    }
}
