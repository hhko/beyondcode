using FluentValidation;
using FunctionalDdd.Framework.BaseTypes.Cqrs;
using LanguageExt;

namespace GymManagement.Application.Usecases.Profiles.Commands.CreateParticipantProfiles;

public static class CreateParticipantProfile
{
    public sealed record Command(
        Guid UserId)
        : ICommand2<Response>;

    public sealed record Response(
        Option<Guid> ParticipantId = default)
        : IResponse;

    internal sealed class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
        }
    }
}