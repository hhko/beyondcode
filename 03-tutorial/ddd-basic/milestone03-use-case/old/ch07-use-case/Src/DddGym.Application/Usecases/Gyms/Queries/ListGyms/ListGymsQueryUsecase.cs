using DddGym.Application.Abstractions.BaseTypes.Cqrs;
using DddGym.Application.Abstractions.Repositories;
using DddGym.Application.Usecases.Gyms.Queries.GetGym;
using DddGym.Domain.AggregateRoots.Gyms;
using ErrorOr;

namespace DddGym.Application.Usecases.Gyms.Queries.ListGyms;

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

//private readonly IGymsRepository _gymsRepository;
//private readonly ISubscriptionsRepository _subscriptionsRepository;

//public ListGymsQueryHandler(IGymsRepository gymsRepository, ISubscriptionsRepository subscriptionsRepository)
//{
//    _gymsRepository = gymsRepository;
//    _subscriptionsRepository = subscriptionsRepository;
//}

//public async Task<ErrorOr<List<Gym>>> Handle(ListGymsQuery query, CancellationToken cancellationToken)
//{
//    if (!await _subscriptionsRepository.ExistsAsync(query.SubscriptionId))
//    {
//        return Error.NotFound(description: "Subscription not found");
//    }

//    return await _gymsRepository.ListSubscriptionGyms(query.SubscriptionId);
//}