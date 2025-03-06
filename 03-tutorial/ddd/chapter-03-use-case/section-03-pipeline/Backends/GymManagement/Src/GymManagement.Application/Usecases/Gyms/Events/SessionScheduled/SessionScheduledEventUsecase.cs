using DddGym.Framework.BaseTypes.Application.Events;
using GymManagement.Application.Abstractions.Repositories;
using GymManagement.Domain.AggregateRoots.Rooms.Events;
using Throw;

namespace GymManagement.Application.Usecases.Gyms.Events.SessionScheduled;

internal sealed class SessionScheduledEventUsecase
    : IDomainEventUsecase<SessionScheduledEvent>
{
    private readonly IGymsRepository _gymsRepository;

    public SessionScheduledEventUsecase(IGymsRepository gymsRepository)
    {
        _gymsRepository = gymsRepository;
    }

    public async Task Handle(SessionScheduledEvent domainEvent, CancellationToken cancellationToken)
    {
        var gym = await _gymsRepository.GetByIdAsync(domainEvent.RoomId);
        gym.ThrowIfNull();

        gym.AddTrainer(domainEvent.Session.TrainerId);
    }
}