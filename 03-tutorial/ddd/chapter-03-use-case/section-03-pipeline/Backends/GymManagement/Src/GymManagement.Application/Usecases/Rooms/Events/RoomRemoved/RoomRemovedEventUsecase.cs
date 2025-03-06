using DddGym.Framework.BaseTypes.Application.Events;
using GymManagement.Application.Abstractions.Repositories;
using GymManagement.Domain.AggregateRoots.Gyms.Events;
using GymManagement.Domain.AggregateRoots.Rooms;
using static GymManagement.Domain.AggregateRoots.Gyms.Errors.DomainEventErrors;


namespace GymManagement.Application.Usecases.Rooms.Events.RoomRemoved;

internal sealed class RoomRemovedEventUsecase
    : IDomainEventUsecase<RoomRemovedEvent>
{
    private readonly IRoomsRepository _roomsRepository;

    public RoomRemovedEventUsecase(IRoomsRepository roomsRepository)
    {
        _roomsRepository = roomsRepository;
    }

    public async Task Handle(RoomRemovedEvent domainEvent, CancellationToken cancellationToken)
    {
        Room room = await _roomsRepository.GetByIdAsync(domainEvent.RoomId)
            ?? throw new DomainEventException(RoomRemovedErrors.RoomNotFound);

        await _roomsRepository.RemoveAsync(room);
    }
}
