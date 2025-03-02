using FluentValidation;

namespace GymManagement.Application.Usecases.Rooms.Queries.GetRoom;

internal sealed class GetRoomQueryValidator : AbstractValidator<GetRoomQuery>
{
    public GetRoomQueryValidator()
    {
        RuleFor(x => x.GymId)
            .NotEmpty();

        RuleFor(x => x.RoomId)
            .NotEmpty();
    }
}