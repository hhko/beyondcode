using DddGym.Framework.BaseTypes.Cqrs;
using GymManagement.Domain.AggregateRoots.Rooms;

namespace GymManagement.Application.Usecases.Rooms.Queries.ListRooms;

//// TODO: LanguageExt
//internal sealed class ListRoomsQueryUsecase
//    : IQueryUsecase<ListRoomsQuery, ListRoomsResponse>
//{
//    private readonly IRoomsRepository _roomsRepository;

//    public ListRoomsQueryUsecase(IRoomsRepository roomsRepository)
//    {
//        _roomsRepository = roomsRepository;
//    }

//    public async Task<IErrorOr<ListRoomsResponse>> Handle(ListRoomsQuery query, CancellationToken cancellationToken)
//    {
//        List<Room> rooms = await _roomsRepository.ListByGymIdAsync(query.GymId);

//        return rooms
//            .ToResponse()
//            .ToErrorOr();
//    }
//}
