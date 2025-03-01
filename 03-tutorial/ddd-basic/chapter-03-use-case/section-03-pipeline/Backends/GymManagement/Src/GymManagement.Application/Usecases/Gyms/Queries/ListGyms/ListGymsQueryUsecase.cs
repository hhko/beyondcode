using DddGym.Framework.BaseTypes.Application.Cqrs;
using ErrorOr;
using GymManagement.Application.Abstractions.Repositories;

namespace GymManagement.Application.Usecases.Gyms.Queries.ListGyms;

internal sealed class ListGymsQueryUsecase
    : IQueryUsecase<ListGymsQuery, ListGymsResponse>
{
    private readonly IGymsRepository _gymsRepository;
    private readonly ISubscriptionsRepository _subscriptionsRepository;

    public ListGymsQueryUsecase(IGymsRepository gymsRepository, ISubscriptionsRepository subscriptionsRepository)
    {
        _gymsRepository = gymsRepository;
        _subscriptionsRepository = subscriptionsRepository;
    }

    public async Task<IErrorOr<ListGymsResponse>> Handle(ListGymsQuery query, CancellationToken cancellationToken)
    {
        if (await _subscriptionsRepository.ExistsAsync(query.SubscriptionId) is false)
        {
            return Error
                .NotFound("Subscription not found")
                .ToErrorOr<ListGymsResponse>();
        }

        var gyms = await _gymsRepository.ListSubscriptionGyms(query.SubscriptionId);

        return gyms
            .ToResponse()
            .ToErrorOr();
    }
}