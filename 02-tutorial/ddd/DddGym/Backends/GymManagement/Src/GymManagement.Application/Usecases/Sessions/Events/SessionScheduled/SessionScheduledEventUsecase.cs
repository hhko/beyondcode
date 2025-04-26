using FunctionalDdd.Framework.BaseTypes.Events;
using GymManagement.Domain.AggregateRoots.Sessions;
using static GymManagement.Domain.AggregateRoots.Rooms.Events.DomainEvents;

namespace GymManagement.Application.Usecases.Sessions.Events.SessionScheduled;

internal sealed class SessionScheduledEventUsecase
    : IDomainEventUsecase<RoomEvents.SessionScheduledEvent>
{
    private readonly ISessionsRepository _sessionsRepository;

    public SessionScheduledEventUsecase(ISessionsRepository sessionsRepository)
    {
        _sessionsRepository = sessionsRepository;
    }

    public async Task Handle(RoomEvents.SessionScheduledEvent domainEvent, CancellationToken cancellationToken)
    {
        await _sessionsRepository.AddSessionAsync(domainEvent.Session);
    }
}
