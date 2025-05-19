using GymDdd.Framework.BaseTypes.Events;
using GymManagement.Domain.AggregateRoots.Sessions;
using static GymManagement.Domain.AggregateRoots.Gyms.Events.DomainEvents;

namespace GymManagement.Application.Usecases.Sessions.Events;

public static class RoomRemovedEvent
{
    internal sealed class Usecase
        : IDomainEventUsecase<GymEvents.RoomRemovedEvent>
    {
        private readonly ISessionsRepository _sessionsRepository;

        public Usecase(ISessionsRepository sessionsRepository)
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
}