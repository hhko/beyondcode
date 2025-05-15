using FluentValidation;
using FunctionalDdd.Framework.BaseTypes.Cqrs;
using GymManagement.Domain.AggregateRoots.Rooms;

namespace GymManagement.Application.Usecases.Rooms.Queries;

public static class ListRoomsQuery
{
    public sealed record Request(
        Guid GymId)
        : IQuery<Response>;

    public sealed record Response(
        List<Room> Rooms)
        : IResponse;

    internal sealed class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.GymId)
                .NotEmpty();
        }
    }

    //// TODO: LanguageExt
    //internal sealed class ListRoomsQueryUsecase
    //    : IQueryUsecase<ListRoomsQuery, ListRoomsResponse>
    //{
    //    private readonly IRoomsRepository _roomsRepository;

    //    public ListRoomsQueryUsecase(IRoomsRepository roomsRepository)
    //    {
    //        _roomsRepository = roomsRepository;
    //    }

    //    public async Task<IErrorOr<ListRoomsResponse>> Handle(ListRoomsQuery query, CancellationToken cancellationToken)
    //    {
    //        List<Room> rooms = await _roomsRepository.ListByGymIdAsync(query.GymId);

    //        return rooms
    //            .ToResponse()
    //            .ToErrorOr();
    //    }
    //}
}