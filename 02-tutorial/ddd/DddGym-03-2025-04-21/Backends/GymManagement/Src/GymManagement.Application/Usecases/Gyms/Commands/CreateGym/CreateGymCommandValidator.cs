using FluentValidation;

namespace GymManagement.Application.Usecases.Gyms.Commands.CreateGym;

internal sealed class CreateGymCommandValidator : AbstractValidator<CreateGymCommand>
{
    public CreateGymCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();

        RuleFor(x => x.SubscriptionId)
            .NotEmpty();
    }
}