using FluentValidation;
using FunctionalDdd.Framework.BaseTypes.Cqrs;
using LanguageExt;

namespace GymManagement.Application.Usecases.Profiles.Commands.CreateTrainerProfiles;

public static class CreateTrainerProfile
{
    public sealed record Command(
        Guid UserId)
        : ICommand2<Response>;

    public sealed record Response(
        Option<Guid> TrainerId = default)
        : IResponse;

    internal sealed class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(_ => _.UserId)
                .NotEmpty();
        }
    }
}