using DddGym.Framework.BaseTypes.Events;
using GymManagement.Application.Abstractions.Repositories;
using GymManagement.Domain.AggregateRoots.Rooms.Events;

namespace GymManagement.Application.Usecases.Sessions.Events.SessionScheduled;

internal sealed class SessionScheduledEventUsecase
    : IDomainEventUsecase<SessionScheduledEvent>
{
    private readonly ISessionsRepository _sessionsRepository;

    public SessionScheduledEventUsecase(ISessionsRepository sessionsRepository)
    {
        _sessionsRepository = sessionsRepository;
    }

    public async Task Handle(SessionScheduledEvent domainEvent, CancellationToken cancellationToken)
    {
        await _sessionsRepository.AddSessionAsync(domainEvent.Session);
    }
}
