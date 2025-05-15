using FluentValidation;

namespace GymManagement.Application.Usecases.Rooms.Commands.DeleteRoom;

internal sealed class DeleteRoomCommandValidator : AbstractValidator<DeleteRoomCommand>
{
    public DeleteRoomCommandValidator()
    {
        RuleFor(x => x.GymId)
            .NotEmpty();

        RuleFor(x => x.RoomId)
            .NotEmpty();
    }
}