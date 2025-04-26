using FluentValidation;
using FunctionalDdd.Framework.BaseTypes.Cqrs;
using LanguageExt;

namespace GymManagement.Application.Usecases.Profiles.Commands.CreateAdminProfiles;

public static class CreateAdminProfile
{
    public sealed record Command(
        Guid UserId)
        : ICommand2<Response>;

    public sealed record Response(
        Option<Guid> AdminId)
        : IResponse;

    internal sealed class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(_ => _.UserId)
                .NotEmpty();
        }
    }

    internal sealed class Telemetry
    {

    }
}
