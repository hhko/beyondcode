using DddGym.Framework.BaseTypes.Events;
using GymManagement.Application.Abstractions.Repositories;
using GymManagement.Domain.AggregateRoots.Gyms.Events;
using GymManagement.Domain.AggregateRoots.Sessions;

namespace GymManagement.Application.Usecases.Sessions.Events.RoomRemoved;

internal sealed class RoomRemovedEventUsecase
    : IDomainEventUsecase<RoomRemovedEvent>
{
    private readonly ISessionsRepository _sessionsRepository;

    public RoomRemovedEventUsecase(ISessionsRepository sessionsRepository)
    {
        _sessionsRepository = sessionsRepository;
    }

    public async Task Handle(RoomRemovedEvent domainEvent, CancellationToken cancellationToken)
    {
        List<Session> sessions = await _sessionsRepository.ListByRoomIdAsync(domainEvent.RoomId);

        sessions.ForEach(session => session.Cancel());

        await _sessionsRepository.RemoveRangeAsync(sessions);
    }
}
