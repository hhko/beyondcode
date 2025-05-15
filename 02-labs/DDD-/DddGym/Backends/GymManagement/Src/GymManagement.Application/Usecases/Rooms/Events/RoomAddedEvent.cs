using FunctionalDdd.Framework.BaseTypes.Events;
using GymManagement.Domain.AggregateRoots.Rooms;
using static GymManagement.Domain.AggregateRoots.Gyms.Events.DomainEvents;

namespace GymManagement.Application.Usecases.Rooms.Events;

public static class RoomAddedEvent
{
    internal sealed class Usecase
        : IDomainEventUsecase<GymEvents.RoomAddedEvent>
    {
        private readonly IRoomsRepository _roomsRepository;

        public Usecase(IRoomsRepository roomsRepository)
        {
            _roomsRepository = roomsRepository;
        }

        public async Task Handle(GymEvents.RoomAddedEvent domainEvent, CancellationToken cancellationToken)
        {
            Room room = Room.Create(
                domainEvent.Name,
                domainEvent.MaxDailySessions,
                domainEvent.GymId,
                id: domainEvent.RoomId);

            await _roomsRepository.AddRoomAsync(room);
        }
    }
}