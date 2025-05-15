using FluentValidation;

namespace GymManagement.Application.Usecases.Rooms.Commands.CreateRoom;

internal sealed class CreateRoomCommandValidator : AbstractValidator<CreateRoomCommand>
{
    public CreateRoomCommandValidator()
    {
        RuleFor(x => x.RoomName)
            .NotEmpty();

        RuleFor(x => x.GymId)
            .NotEmpty();
    }
}