using DddGym.Framework.BaseTypes.Cqrs;
using ErrorOr;
using GymManagement.Domain.AggregateRoots.Subscriptions;

namespace GymManagement.Application.Usecases.Subscriptions.Queries.ListSubscriptions;

internal sealed class ListSubscriptionsQueryUsecase
    : IQueryUsecase<ListSubscriptionsQuery, ListSubscriptionsResponse>
{
    private readonly ISubscriptionRepository _subscriptionRepository;

    public ListSubscriptionsQueryUsecase(ISubscriptionRepository subscriptionRepository)
    {
        _subscriptionRepository = subscriptionRepository;
    }

    public async Task<IErrorOr<ListSubscriptionsResponse>> Handle(ListSubscriptionsQuery query, CancellationToken cancellationToken)
    {
        var subscriptions = await _subscriptionRepository.ListAsync();

        return subscriptions
            .ToResponse()
            .ToErrorOr();
    }
}