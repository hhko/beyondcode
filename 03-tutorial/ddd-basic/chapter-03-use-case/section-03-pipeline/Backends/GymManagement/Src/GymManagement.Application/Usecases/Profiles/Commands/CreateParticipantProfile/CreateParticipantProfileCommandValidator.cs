using FluentValidation;

namespace GymManagement.Application.Usecases.Users.Commands.CreateParticipantProfile;

internal sealed class CreateParticipantProfileCommandValidator : AbstractValidator<CreateParticipantProfileCommand>
{
    public CreateParticipantProfileCommandValidator()
    {
    }
}