using FluentValidation;

namespace GymManagement.Application.Usecases.Trainers.Commands.AddTrainer;

internal sealed class AddTrainerCommandValidator : AbstractValidator<AddTrainerCommand>
{
    public AddTrainerCommandValidator()
    {
        RuleFor(x => x.SubscriptionId)
            .NotEmpty();

        RuleFor(x => x.GymId)
            .NotEmpty();

        RuleFor(x => x.TrainerId)
            .NotEmpty();
    }
}
