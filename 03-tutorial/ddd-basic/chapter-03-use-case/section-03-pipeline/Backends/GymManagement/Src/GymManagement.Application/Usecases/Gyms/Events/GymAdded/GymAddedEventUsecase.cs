using DddGym.Framework.BaseTypes.Application.Events;
using GymManagement.Application.Abstractions.Repositories;
using GymManagement.Domain.AggregateRoots.Subscriptions.Events;

namespace GymManagement.Application.Usecases.Gyms.Events.GymAdded;

internal sealed class GymAddedEventUsecase
    : IDomainEventUsecase<GymAddedEvent>
{
    private readonly IGymsRepository _gymsRepository;

    public GymAddedEventUsecase(IGymsRepository gymsRepository)
    {
        _gymsRepository = gymsRepository;
    }

    // TODO? 도메인 이벤트를 이용하여 AggregateRoot을 저장하고 있다?
    public async Task Handle(GymAddedEvent domainEvent, CancellationToken cancellationToken)
    {
        await _gymsRepository.AddGymAsync(domainEvent.Gym);
    }
}