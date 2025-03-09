using FluentValidation;

namespace GymManagement.Application.Usecases.Profiles.Commands.CreateParticipantProfile;

internal sealed class CreateParticipantProfileCommandValidator : AbstractValidator<CreateParticipantProfileCommand>
{
    public CreateParticipantProfileCommandValidator()
    {
    }
}