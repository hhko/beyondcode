using FluentValidation;
using GymDdd.Framework.BaseTypes.Cqrs;
using GymManagement.Domain.AggregateRoots.Sessions;

namespace GymManagement.Application.Usecases.Sessions.Queries;

public static class GetSessionQuery
{
    public sealed record Request(
        Guid RoomId,
        Guid SessionId)
        : IQuery<Response>;

    public sealed record Response(
        Session Session)
        : IResponse;

    internal sealed class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.RoomId)
                .NotEmpty();

            RuleFor(x => x.SessionId)
                .NotEmpty();
        }
    }

    //// TODO: LanguageExt
    //internal sealed class GetSessionQueryUsecase
    //    : IQueryUsecase<GetSessionQuery, GetSessionResponse>
    //{
    //    private readonly IRoomsRepository _roomsRepository;
    //    private readonly ISessionsRepository _sessionsRepository;

    //    public GetSessionQueryUsecase(
    //        IRoomsRepository roomsRepository,
    //        ISessionsRepository sessionsRepository)
    //    {
    //        _roomsRepository = roomsRepository;
    //        _sessionsRepository = sessionsRepository;
    //    }

    //    public async Task<IErrorOr<GetSessionResponse>> Handle(GetSessionQuery query, CancellationToken cancellationToken)
    //    {
    //        Room? room = await _roomsRepository.GetByIdAsync(query.RoomId);
    //        if (room is null)
    //        {
    //            return Error
    //                .NotFound(description: "Room not found")
    //                .ToErrorOr<GetSessionResponse>();
    //        }

    //        if (!room.HasSession(query.SessionId))
    //        {
    //            return Error
    //                .NotFound(description: "Session not found")
    //                .ToErrorOr<GetSessionResponse>();
    //        }

    //        Session? session = await _sessionsRepository.GetByIdAsync(query.SessionId);
    //        if (session is null)
    //        {
    //            return Error
    //                .NotFound(description: "Session not found")
    //                .ToErrorOr<GetSessionResponse>();
    //        }

    //        return session
    //            .ToResponse()
    //            .ToErrorOr();
    //    }
    //}
}