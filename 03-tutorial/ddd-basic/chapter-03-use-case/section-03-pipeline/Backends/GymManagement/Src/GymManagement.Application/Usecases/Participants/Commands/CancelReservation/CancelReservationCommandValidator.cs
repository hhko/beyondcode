using FluentValidation;

namespace GymManagement.Application.Usecases.Participants.Commands.CancelReservation;

internal sealed class CancelReservationCommandValidator : AbstractValidator<CancelReservationCommand>
{
    public CancelReservationCommandValidator()
    {
    }
}