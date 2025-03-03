using FluentValidation;

namespace GymManagement.Application.Usecases.Sessions.Commands.CreateReservation;

internal sealed class CreateReservationCommandValidator : AbstractValidator<CreateReservationCommand>
{
    public CreateReservationCommandValidator()
    {
        RuleFor(x => x.SessionId)
            .NotEmpty();

        RuleFor(x => x.ParticipantId)
            .NotEmpty();
    }
}