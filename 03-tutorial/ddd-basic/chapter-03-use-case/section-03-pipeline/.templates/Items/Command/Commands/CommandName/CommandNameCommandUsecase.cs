using HostName.Application.Abstractions.BaseTypes.Cqrs;
using HostName.Application.Abstractions.Repositories;
using HostName.Domain.AggregateRoots.EntityNames;
using HostName.Domain.AggregateRoots.Subscriptions;
using ErrorOr;

namespace HostName.Application.Usecases.EntityNames.Commands.CommandName;

internal sealed class CommandNameCommandUsecase
    : ICommandusecase<CommandNameCommand, CommandNameResponse>
{
    private readonly ISubscriptionsRepository _subscriptionsRepository;

    public CommandNameCommandUsecase(ISubscriptionsRepository subscriptionsRepository)
    {
        _subscriptionsRepository = subscriptionsRepository;
    }

    public async Task<IErrorOr<CommandNameResponse>> Handle(CommandNameCommand command, CancellationToken cancellationToken)
    {
        if (await _subscriptionsRepository.GetByIdAsync(command.SubscriptionId) is not Subscription subscription)
        {
            return Error
                .NotFound(description: "Subscription not found")
                .ToErrorOr<CommandNameResponse>();
        }

        // var gym = new Gym(
        //     name: command.Name,
        //     maxRooms: subscription.GetMaxRooms(),
        //     subscriptionId: subscription.Id);

        // var addGymResult = subscription.AddGym(gym);
        // if (addGymResult.IsError)
        // {
        //     return addGymResult
        //         .Errors
        //         .ToErrorOr<CommandNameResponse>();
        // }

        await _subscriptionsRepository.UpdateAsync(subscription);


    }
}
