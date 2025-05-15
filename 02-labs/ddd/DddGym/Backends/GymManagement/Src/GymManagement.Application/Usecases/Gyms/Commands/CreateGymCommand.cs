using FluentValidation;
using FunctionalDdd.Framework.BaseTypes.Cqrs;
using GymManagement.Domain.AggregateRoots.Gyms;

namespace GymManagement.Application.Usecases.Gyms.Commands;

public static class CreateGymCommand
{
    public sealed record Request(
        string Name,
        Guid SubscriptionId)
        : ICommand<Response>;

    public sealed record Response(
        Gym gym)
        : IResponse;

    internal sealed class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();

            RuleFor(x => x.SubscriptionId)
                .NotEmpty();
        }
    }

    //// TODO: LanguageExt
    //internal sealed class CreateGymCommandUsecase
    //    : ICommandUsecase<CreateGymCommand, CreateGymResponse>
    //{
    //    private readonly ISubscriptionsRepository _subscriptionsRepository;

    //    public CreateGymCommandUsecase(ISubscriptionsRepository subscriptionsRepository)
    //    {
    //        _subscriptionsRepository = subscriptionsRepository;
    //    }

    //    public async Task<IErrorOr<CreateGymResponse>> Handle(CreateGymCommand command, CancellationToken cancellationToken)
    //    {
    //        Subscription? subscription = await _subscriptionsRepository.GetByIdAsync(command.SubscriptionId);
    //        if (subscription == null)
    //        {
    //            return Error
    //                .NotFound(description: "Subscription not found")
    //                .ToErrorOr<CreateGymResponse>();
    //        }

    //        var gym = new Gym(
    //            name: command.Name,
    //            maxRooms: subscription.GetMaxRooms(),
    //            subscriptionId: subscription.Id);

    //        var addGymResult = subscription.AddGym(gym);
    //        if (addGymResult.IsError)
    //        {
    //            return addGymResult
    //                .Errors
    //                .ToErrorOr<CreateGymResponse>();
    //        }

    //        await _subscriptionsRepository.UpdateAsync(subscription);

    //        return gym
    //            .ToResponseCreated()
    //            .ToErrorOr();
    //    }
    //}
}