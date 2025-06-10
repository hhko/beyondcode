﻿using DddGym.Framework.BaseTypes.Cqrs;
using ErrorOr;
using GymManagement.Domain.AggregateRoots.Gyms;

//using GymManagement.Application.Abstractions.Repositories;
using GymManagement.Domain.AggregateRoots.Subscriptions;

namespace GymManagement.Application.Usecases.Gyms.Commands.CreateGym;

internal sealed class CreateGymCommandUsecase
    : ICommandUsecase<CreateGymCommand, CreateGymResponse>
{
    private readonly ISubscriptionsRepository _subscriptionsRepository;

    public CreateGymCommandUsecase(ISubscriptionsRepository subscriptionsRepository)
    {
        _subscriptionsRepository = subscriptionsRepository;
    }

    public async Task<IErrorOr<CreateGymResponse>> Handle(CreateGymCommand command, CancellationToken cancellationToken)
    {
        Subscription? subscription = await _subscriptionsRepository.GetByIdAsync(command.SubscriptionId);
        if (subscription == null)
        {
            return Error
                .NotFound(description: "Subscription not found")
                .ToErrorOr<CreateGymResponse>();
        }

        var gym = new Gym(
            name: command.Name,
            maxRooms: subscription.GetMaxRooms(),
            subscriptionId: subscription.Id);

        var addGymResult = subscription.AddGym(gym);
        if (addGymResult.IsError)
        {
            return addGymResult
                .Errors
                .ToErrorOr<CreateGymResponse>();
        }

        await _subscriptionsRepository.UpdateAsync(subscription);

        return gym
            .ToResponseCreated()
            .ToErrorOr();
    }
}