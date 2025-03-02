using FluentValidation;

namespace GymManagement.Application.Usecases.Rooms.Queries.ListRooms;

internal sealed class ListRoomsQueryValidator : AbstractValidator<ListRoomsQuery>
{
    public ListRoomsQueryValidator()
    {
    }
}