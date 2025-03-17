using DddGym.Framework.BaseTypes.Cqrs;
using ErrorOr;
using GymManagement.Domain.AggregateRoots.Admins;
using GymManagement.Domain.AggregateRoots.Subscriptions;

namespace GymManagement.Application.Usecases.Subscriptions.Commands.CreateSubscription;

internal sealed class CreateSubscriptionCommandUsecase
    : ICommandUsecase<CreateSubscriptionCommand, CreateSubscriptionResponse>
{
    private readonly IAdminsRepository _adminsRepository;

    public CreateSubscriptionCommandUsecase(IAdminsRepository adminsRepository)
    {
        _adminsRepository = adminsRepository;
    }

    public async Task<IErrorOr<CreateSubscriptionResponse>> Handle(CreateSubscriptionCommand command, CancellationToken cancellationToken)
    {
        var admin = await _adminsRepository.GetByIdAsync(command.AddminId);
        if (admin is null)
        {
            return Error
                .NotFound(description: "Admin not found")
                .ToErrorOr<CreateSubscriptionResponse>();
        }

        if (admin.SubscriptionId is not null)
        {
            return Error
                .Conflict(description: "Admin already has active subscription")
                .ToErrorOr<CreateSubscriptionResponse>();
        }

        var subscription = new Subscription(command.SubscriptionType, command.AddminId);
        admin.SetSubscription(subscription);

        await _adminsRepository.UpdateAsync(admin);

        return subscription
            .ToResponse()
            .ToErrorOr();
    }
}