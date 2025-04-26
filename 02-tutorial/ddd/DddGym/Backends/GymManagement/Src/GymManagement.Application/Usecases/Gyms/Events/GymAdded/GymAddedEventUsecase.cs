using FunctionalDdd.Framework.BaseTypes.Events;
using GymManagement.Domain.AggregateRoots.Gyms;
using static GymManagement.Domain.AggregateRoots.Subscriptions.Events.DomainEvents;

namespace GymManagement.Application.Usecases.Gyms.Events.GymAdded;

internal sealed class GymAddedEventUsecase
    : IDomainEventUsecase<SubscriptionEvents.GymAddedEvent>
{
    private readonly IGymsRepository _gymsRepository;

    public GymAddedEventUsecase(IGymsRepository gymsRepository)
    {
        _gymsRepository = gymsRepository;
    }

    // TODO? 도메인 이벤트를 이용하여 AggregateRoot을 저장하고 있다?
    public async Task Handle(SubscriptionEvents.GymAddedEvent domainEvent, CancellationToken cancellationToken)
    {
        await _gymsRepository.AddGymAsync(domainEvent.Gym);
    }
}