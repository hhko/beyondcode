using FunctionalDdd.Framework.BaseTypes.Events;
using GymManagement.Domain.AggregateRoots.Sessions;
using static GymManagement.Domain.AggregateRoots.Rooms.Events.DomainEvents;

namespace GymManagement.Application.Usecases.Sessions.Events;

public static class SessionScheduledEvent
{
    internal sealed class Usecase
        : IDomainEventUsecase<RoomEvents.SessionScheduledEvent>
    {
        private readonly ISessionsRepository _sessionsRepository;

        public Usecase(ISessionsRepository sessionsRepository)
        {
            _sessionsRepository = sessionsRepository;
        }

        public async Task Handle(RoomEvents.SessionScheduledEvent domainEvent, CancellationToken cancellationToken)
        {
            await _sessionsRepository.AddSessionAsync(domainEvent.Session);
        }
    }
}