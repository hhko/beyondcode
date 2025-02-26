using GymManagement.Application.Abstractions.Repositories;
using GymManagement.Domain.AggregateRoots.Subscriptions;
using ErrorOr;
using GymManagement.Domain.AggregateRoots.Gyms;
using GymManagement.Application.Usecases.Gyms;
using DddGym.Framework.BaseTypes.Application.Cqrs;

namespace GymManagement.Application.Usecases.Trainers.Commands.AddTrainer;

internal sealed class AddTrainerCommandUsecase
    : ICommandusecase<AddTrainerCommand, AddTrainerResponse>
{
    private readonly ISubscriptionsRepository _subscriptionsRepository;
    private readonly IGymsRepository _gymsRepository;

    public AddTrainerCommandUsecase(
        ISubscriptionsRepository subscriptionsRepository,
        IGymsRepository gymsRepository)
    {
        _subscriptionsRepository = subscriptionsRepository;
        _gymsRepository = gymsRepository;
    }

    public async Task<IErrorOr<AddTrainerResponse>> Handle(AddTrainerCommand command, CancellationToken cancellationToken)
    {
        if (await _subscriptionsRepository.GetByIdAsync(command.SubscriptionId) is not Subscription subscription)
        {
            return Error
                .NotFound(description: "Subscription not found")
                .ToErrorOr<AddTrainerResponse>();
        }

        // TODO: 언제 객체에서 찾고, 언제 DB에서 찾니?
        if (!subscription.HasGym(command.GymId))
        {
            return Error
                .NotFound(description: "Gym not found")
                .ToErrorOr<AddTrainerResponse>();
        }

        if (await _gymsRepository.GetByIdAsync(command.GymId) is not Gym gym)
        {
            return Error
                .NotFound(description: "Gym not found")
                .ToErrorOr<AddTrainerResponse>();
        }

        var addTrainerResult = gym.AddTrainer(command.TrainerId);
        if (addTrainerResult.IsError)
        {
            return addTrainerResult
                .Errors
                .ToErrorOr<AddTrainerResponse>();
        }

        // TODO: DB에 gym 객체 전체를 Update하지 않고, 새로 생성된 Trainer만 Update할 수 없나?
        await _gymsRepository.UpdateAsync(gym);

        return Result.Success
            .ToResponseAdded()
            .ToErrorOr();
    }
}
