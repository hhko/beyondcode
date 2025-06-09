﻿using DddGym.Framework.BaseTypes.Cqrs;
using ErrorOr;
using GymManagement.Domain.AggregateRoots.Gyms;
using GymManagement.Domain.AggregateRoots.Subscriptions;

namespace GymManagement.Application.Usecases.Gyms.Queries.GetGym;

internal sealed class GetGymQueryUsecase
    : IQueryUsecase<GetGymQuery, GetGymResponse>
{
    private readonly IGymsRepository _gymsRepository;
    private readonly ISubscriptionsRepository _subscriptionsRepository;

    public GetGymQueryUsecase(IGymsRepository gymsRepository, ISubscriptionsRepository subscriptionsRepository)
    {
        _gymsRepository = gymsRepository;
        _subscriptionsRepository = subscriptionsRepository;
    }

    public async Task<IErrorOr<GetGymResponse>> Handle(GetGymQuery query, CancellationToken cancellationToken)
    {
        if (await _subscriptionsRepository.ExistsAsync(query.SubscriptionId) is false)
        {
            return Error
                .NotFound("Subscription not found")
                .ToErrorOr<GetGymResponse>();
        }

        if (await _gymsRepository.GetByIdAsync(query.GymId) is not Gym gym)
        {
            return Error
                .NotFound("Gym not found")
                .ToErrorOr<GetGymResponse>();
        }

        return gym
            .ToResponse()
            .ToErrorOr();
    }
}