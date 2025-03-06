using DddGym.Framework.BaseTypes.Application.Events;
using GymManagement.Application.Abstractions.Repositories;
using GymManagement.Domain.AggregateRoots.Admins.Events;

namespace GymManagement.Application.Usecases.Subscriptions.Events.SubscriptionSet;

internal sealed class SubscriptionSetEventUsecase
    : IDomainEventUsecase<SubscriptionSetEvent>
{
    private readonly ISubscriptionsRepository _subscriptionsRepository;

    public SubscriptionSetEventUsecase(ISubscriptionsRepository subscriptionsRepository)
    {
        _subscriptionsRepository = subscriptionsRepository;
    }

    public async Task Handle(SubscriptionSetEvent domainEvent, CancellationToken cancellationToken)
    {
        await _subscriptionsRepository.AddSubscriptionAsync(domainEvent.Subscription);
    }
}