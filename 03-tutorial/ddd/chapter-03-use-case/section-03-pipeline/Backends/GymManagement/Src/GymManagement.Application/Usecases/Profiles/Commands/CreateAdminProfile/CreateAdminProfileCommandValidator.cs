using FluentValidation;

namespace GymManagement.Application.Usecases.Users.Commands.CreateAdminProfile;

internal sealed class CreateAdminProfileCommandValidator : AbstractValidator<CreateAdminProfileCommand>
{
    public CreateAdminProfileCommandValidator()
    {
    }
}