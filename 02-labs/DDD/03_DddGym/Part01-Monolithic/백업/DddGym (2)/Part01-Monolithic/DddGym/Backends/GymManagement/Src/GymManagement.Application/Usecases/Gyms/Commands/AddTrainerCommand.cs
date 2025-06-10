using FluentValidation;
using GymDdd.Framework.BaseTypes.Cqrs;

namespace GymManagement.Application.Usecases.Gyms.Commands;

public static class AddTrainerCommand
{
    public sealed record Request(
        Guid SubscriptionId,
        Guid GymId,
        Guid TrainerId)
        : ICommandReqeust<Response>;

    public sealed record Response()
        : IResponse;

    internal sealed class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.SubscriptionId)
                .NotEmpty();

            RuleFor(x => x.GymId)
                .NotEmpty();

            RuleFor(x => x.TrainerId)
                .NotEmpty();
        }
    }

    //// TODO: LanguageExt
    //internal sealed class AddTrainerCommandUsecase
    //    : ICommandUsecase<AddTrainerCommand>
    //{
    //    private readonly ISubscriptionsRepository _subscriptionsRepository;
    //    private readonly IGymsRepository _gymsRepository;

    //    public AddTrainerCommandUsecase(
    //        ISubscriptionsRepository subscriptionsRepository,
    //        IGymsRepository gymsRepository)
    //    {
    //        _subscriptionsRepository = subscriptionsRepository;
    //        _gymsRepository = gymsRepository;
    //    }

    //    public async Task<IErrorOr> Handle(AddTrainerCommand command, CancellationToken cancellationToken)
    //    {
    //        if (await _subscriptionsRepository.GetByIdAsync(command.SubscriptionId) is not Subscription subscription)
    //        {
    //            return Error
    //                .NotFound(description: "Subscription not found")
    //                .ToErrorOr<Success>();
    //        }

    //        // TODO: 언제 객체에서 찾고, 언제 DB에서 찾니?
    //        if (!subscription.HasGym(command.GymId))
    //        {
    //            return Error
    //                .NotFound(description: "Gym not found")
    //                .ToErrorOr<Success>();
    //        }

    //        if (await _gymsRepository.GetByIdAsync(command.GymId) is not Gym gym)
    //        {
    //            return Error
    //                .NotFound(description: "Gym not found")
    //                .ToErrorOr<Success>();
    //        }

    //        var addTrainerResult = gym.AddTrainer(command.TrainerId);
    //        if (addTrainerResult.IsError)
    //        {
    //            return addTrainerResult
    //                .Errors
    //                .ToErrorOr<Success>();
    //        }

    //        // TODO: DB에 gym 객체 전체를 Update하지 않고, 새로 생성된 Trainer만 Update할 수 없나?
    //        await _gymsRepository.UpdateAsync(gym);

    //        return Result.Success
    //            .ToErrorOr();
    //    }
    //}
}
