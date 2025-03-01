using DddGym.Framework.BaseTypes.Application.Events;
using DddGym.Framework.IntegrationEvents;
using GymManagement.Application.Abstractions.Repositories;
using Throw;

namespace GymManagement.Application.Usecases.Gyms.IntegrationEvents.SessionScheduled;

internal sealed class SessionScheduledIntegrationEventUsecase
    : IDomainEventUsecase<SessionScheduledIntegrationEvent>
{
    private readonly IGymsRepository _gymsRepository;

    public SessionScheduledIntegrationEventUsecase(IGymsRepository gymsRepository)
    {
        _gymsRepository = gymsRepository;
    }

    public async Task Handle(SessionScheduledIntegrationEvent domainEvent, CancellationToken cancellationToken)
    {
        var gym = await _gymsRepository.GetByIdAsync(domainEvent.RoomId);
        gym.ThrowIfNull();

        gym.AddTrainer(domainEvent.TrainerId);
    }
}