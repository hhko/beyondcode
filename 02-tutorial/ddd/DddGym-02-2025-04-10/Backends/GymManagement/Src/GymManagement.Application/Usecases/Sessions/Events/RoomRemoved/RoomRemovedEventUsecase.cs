using DddGym.Framework.BaseTypes.Events;
using GymManagement.Domain.AggregateRoots.Sessions;
using static GymManagement.Domain.AggregateRoots.Gyms.Events.DomainEvents;

namespace GymManagement.Application.Usecases.Sessions.Events.RoomRemoved;

internal sealed class RoomRemovedEventUsecase
    : IDomainEventUsecase<GymEvents.RoomRemovedEvent>
{
    private readonly ISessionsRepository _sessionsRepository;

    public RoomRemovedEventUsecase(ISessionsRepository sessionsRepository)
    {
        _sessionsRepository = sessionsRepository;
    }

    public async Task Handle(GymEvents.RoomRemovedEvent domainEvent, CancellationToken cancellationToken)
    {
        List<Session> sessions = await _sessionsRepository.ListByRoomIdAsync(domainEvent.RoomId);

        sessions.ForEach(session => session.Cancel());

        await _sessionsRepository.RemoveRangeAsync(sessions);
    }
}
