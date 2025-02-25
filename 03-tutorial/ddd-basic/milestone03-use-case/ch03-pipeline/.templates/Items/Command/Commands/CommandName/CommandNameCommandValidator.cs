using FluentValidation;

namespace HostName.Application.Usecases.EntityNames.Commands.CommandName;

internal sealed class CommandNameCommandValidator : AbstractValidator<CommandNameCommand>
{
    public CommandNameCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();

        RuleFor(x => x.SubscriptionId)
            .NotEmpty();
    }
}
