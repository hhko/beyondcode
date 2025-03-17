using DddGym.Framework.BaseTypes.Cqrs;
using ErrorOr;
using GymManagement.Domain.AggregateRoots.Rooms;

namespace GymManagement.Application.Usecases.Rooms.Queries.ListRooms;

internal sealed class ListRoomsQueryUsecase
    : IQueryUsecase<ListRoomsQuery, ListRoomsResponse>
{
    private readonly IRoomsRepository _roomsRepository;

    public ListRoomsQueryUsecase(IRoomsRepository roomsRepository)
    {
        _roomsRepository = roomsRepository;
    }

    public async Task<IErrorOr<ListRoomsResponse>> Handle(ListRoomsQuery query, CancellationToken cancellationToken)
    {
        List<Room> rooms = await _roomsRepository.ListByGymIdAsync(query.GymId);

        return rooms
            .ToResponse()
            .ToErrorOr();
    }
}
