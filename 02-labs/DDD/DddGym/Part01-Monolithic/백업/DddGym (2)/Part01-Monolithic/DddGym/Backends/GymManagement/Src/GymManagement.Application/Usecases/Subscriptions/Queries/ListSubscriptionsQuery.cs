using FluentValidation;
using GymDdd.Framework.BaseTypes.Cqrs;
using GymManagement.Domain.AggregateRoots.Subscriptions;

namespace GymManagement.Application.Usecases.Subscriptions.Queries;

public static class ListSubscriptionsQuery
{
    public sealed record Request()
        : IQueryRequest<Response>;

    public sealed record Response(
        List<Subscription> Subscriptions)
        : IResponse;

    internal sealed class Validator : AbstractValidator<Request>
    {
    }

    //internal sealed class ListSubscriptionsQueryUsecase
    //    : IQueryUsecase<ListSubscriptionsQuery, ListSubscriptionsResponse>
    //{
    //    private readonly ISubscriptionRepository _subscriptionRepository;

    //    public ListSubscriptionsQueryUsecase(ISubscriptionRepository subscriptionRepository)
    //    {
    //        _subscriptionRepository = subscriptionRepository;
    //    }

    //    public async Task<IErrorOr<ListSubscriptionsResponse>> Handle(ListSubscriptionsQuery query, CancellationToken cancellationToken)
    //    {
    //        var subscriptions = await _subscriptionRepository.ListAsync();

    //        return subscriptions
    //            .ToResponse()
    //            .ToErrorOr();
    //    }
    //}
}