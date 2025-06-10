using FluentValidation;
using GymDdd.Framework.BaseTypes.Cqrs;
using GymManagement.Domain.AggregateRoots.Rooms;

namespace GymManagement.Application.Usecases.Rooms.Queries;

public static class GetRoomQuery
{
    public sealed record Request(
        Guid GymId,
        Guid RoomId)
        : IQueryRequest<Response>;

    public sealed record Response(
        Room romm)
        : IResponse;

    internal sealed class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.GymId)
                .NotEmpty();

            RuleFor(x => x.RoomId)
                .NotEmpty();
        }
    }

    //// TODO: LanguageExt
    //internal sealed class GetRoomQueryUsecase
    //    : IQueryUsecase<GetRoomQuery, GetRoomResponse>
    //{
    //    private readonly IRoomsRepository _roomsRepository;

    //    public GetRoomQueryUsecase(IRoomsRepository roomsRepository)
    //    {
    //        _roomsRepository = roomsRepository;
    //    }

    //    public async Task<IErrorOr<GetRoomResponse>> Handle(GetRoomQuery query, CancellationToken cancellationToken)
    //    {
    //        Room? room = await _roomsRepository.GetByIdAsync(query.RoomId);
    //        if (room is null)
    //        {
    //            return Error
    //                .NotFound(description: "Room not found")
    //                .ToErrorOr<GetRoomResponse>();
    //        }

    //        // TODO: Response 없이 바로 room 전달 
    //        return room
    //            .ToResponse()
    //            .ToErrorOr();
    //    }
    //}
}