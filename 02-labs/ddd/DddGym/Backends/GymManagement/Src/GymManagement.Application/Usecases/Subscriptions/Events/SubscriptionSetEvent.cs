using FunctionalDdd.Framework.BaseTypes.Events;
using GymManagement.Domain.AggregateRoots.Subscriptions;
using static GymManagement.Domain.AggregateRoots.Admins.Events.DomainEvents;

namespace GymManagement.Application.Usecases.Subscriptions.Events;

public static class SubscriptionSetEvent
{
    internal sealed class Usecase
        : IDomainEventUsecase<AdminEvents.SubscriptionSetEvent>
    {
        private readonly ISubscriptionsRepository _subscriptionsRepository;

        public Usecase(ISubscriptionsRepository subscriptionsRepository)
        {
            _subscriptionsRepository = subscriptionsRepository;
        }

        public async Task Handle(AdminEvents.SubscriptionSetEvent domainEvent, CancellationToken cancellationToken)
        {
            await _subscriptionsRepository.AddSubscriptionAsync(domainEvent.Subscription);
        }
    }
}