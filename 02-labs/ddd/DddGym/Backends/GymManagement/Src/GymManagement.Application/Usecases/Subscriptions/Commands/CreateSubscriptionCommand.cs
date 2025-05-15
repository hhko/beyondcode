using FluentValidation;
using FunctionalDdd.Framework.BaseTypes.Cqrs;
using GymManagement.Domain.AggregateRoots.Subscriptions;
using GymManagement.Domain.AggregateRoots.Subscriptions.Enumerations;

namespace GymManagement.Application.Usecases.Subscriptions.Commands;

public static class CreateSubscriptionCommand
{
    public sealed record Request(
        SubscriptionType SubscriptionType,
        Guid AddminId)
        : ICommand<Response>;

    public sealed record Response(
        Subscription Subscription)
        : IResponse;

    internal sealed class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.AddminId)
                .NotEmpty();

            // TODO: Ardalis.SmartEnum Validator
            // 
        }
    }

    //// TODO: LanguageExt
    //internal sealed class CreateSubscriptionCommandUsecase
    //    : ICommandUsecase<CreateSubscriptionCommand, CreateSubscriptionResponse>
    //{
    //    private readonly IAdminsRepository _adminsRepository;

    //    public CreateSubscriptionCommandUsecase(IAdminsRepository adminsRepository)
    //    {
    //        _adminsRepository = adminsRepository;
    //    }

    //    public async Task<IErrorOr<CreateSubscriptionResponse>> Handle(CreateSubscriptionCommand command, CancellationToken cancellationToken)
    //    {
    //        var admin = await _adminsRepository.GetByIdAsync(command.AddminId);
    //        if (admin is null)
    //        {
    //            return Error
    //                .NotFound(description: "Admin not found")
    //                .ToErrorOr<CreateSubscriptionResponse>();
    //        }

    //        if (admin.SubscriptionId is not null)
    //        {
    //            return Error
    //                .Conflict(description: "Admin already has active subscription")
    //                .ToErrorOr<CreateSubscriptionResponse>();
    //        }

    //        var subscription = new Subscription(command.SubscriptionType, command.AddminId);
    //        admin.SetSubscription(subscription);

    //        await _adminsRepository.UpdateAsync(admin);

    //        return subscription
    //            .ToResponse()
    //            .ToErrorOr();
    //    }
    //}
}