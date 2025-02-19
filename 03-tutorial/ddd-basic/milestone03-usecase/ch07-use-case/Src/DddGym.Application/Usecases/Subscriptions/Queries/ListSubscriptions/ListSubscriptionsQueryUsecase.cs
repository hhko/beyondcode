using DddGym.Application.Abstractions.BaseTypes.Cqrs;
using DddGym.Domain.AggregateRoots.Subscriptions;
using ErrorOr;
using DddGym.Application.Usecases.Subscriptions.Mappings;

namespace DddGym.Application.Usecases.Subscriptions.Queries.ListSubscriptions;

internal sealed class ListSubscriptionsQueryUsecase : IQueryUsecase<ListSubscriptionsQuery, SubscriptionsResponse>
{
    private readonly ISubscriptionRepository _subscriptionRepository;

    public ListSubscriptionsQueryUsecase(ISubscriptionRepository subscriptionRepository)
    {
        _subscriptionRepository = subscriptionRepository;
    }

    public async Task<IErrorOr<SubscriptionsResponse>> Handle(ListSubscriptionsQuery query, CancellationToken cancellationToken)
    {
        var subscriptions = await _subscriptionRepository.ListAsync();

        return subscriptions.ToResponse();
    }
}
