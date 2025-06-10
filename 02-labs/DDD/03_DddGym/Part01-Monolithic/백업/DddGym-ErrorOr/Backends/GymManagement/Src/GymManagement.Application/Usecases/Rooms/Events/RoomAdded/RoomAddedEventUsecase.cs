using DddGym.Framework.BaseTypes.Events;
using GymManagement.Domain.AggregateRoots.Gyms.Events;
using GymManagement.Domain.AggregateRoots.Rooms;

namespace GymManagement.Application.Usecases.Rooms.Events.RoomAdded;

internal sealed class RoomAddedEventUsecase
    : IDomainEventUsecase<RoomAddedEvent>
{
    private readonly IRoomsRepository _roomsRepository;

    public RoomAddedEventUsecase(IRoomsRepository roomsRepository)
    {
        _roomsRepository = roomsRepository;
    }

    public async Task Handle(RoomAddedEvent domainEvent, CancellationToken cancellationToken)
    {
        Room room = new Room(
            domainEvent.Name,
            domainEvent.MaxDailySessions,
            domainEvent.GymId,
            id: domainEvent.RoomId);

        await _roomsRepository.AddRoomAsync(room);
    }
}
