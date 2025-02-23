using FluentValidation;

namespace DddGym.Application.Usecases.Subscriptions.Commands.CreateSubscription;

internal sealed class CreateSubscriptionCommandValidator : AbstractValidator<CreateSubscriptionCommand>
{
    public CreateSubscriptionCommandValidator()
    {
        RuleFor(x => x.AddminId)
            .NotEmpty();
    }
}
