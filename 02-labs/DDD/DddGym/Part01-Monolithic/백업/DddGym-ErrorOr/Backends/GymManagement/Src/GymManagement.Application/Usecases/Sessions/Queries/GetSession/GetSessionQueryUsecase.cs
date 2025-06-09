using DddGym.Framework.BaseTypes.Cqrs;
using ErrorOr;
using GymManagement.Domain.AggregateRoots.Rooms;
using GymManagement.Domain.AggregateRoots.Sessions;

namespace GymManagement.Application.Usecases.Sessions.Queries.GetSession;

internal sealed class GetSessionQueryUsecase
    : IQueryUsecase<GetSessionQuery, GetSessionResponse>
{
    private readonly IRoomsRepository _roomsRepository;
    private readonly ISessionsRepository _sessionsRepository;

    public GetSessionQueryUsecase(
        IRoomsRepository roomsRepository,
        ISessionsRepository sessionsRepository)
    {
        _roomsRepository = roomsRepository;
        _sessionsRepository = sessionsRepository;
    }

    public async Task<IErrorOr<GetSessionResponse>> Handle(GetSessionQuery query, CancellationToken cancellationToken)
    {
        Room? room = await _roomsRepository.GetByIdAsync(query.RoomId);
        if (room is null)
        {
            return Error
                .NotFound(description: "Room not found")
                .ToErrorOr<GetSessionResponse>();
        }

        if (!room.HasSession(query.SessionId))
        {
            return Error
                .NotFound(description: "Session not found")
                .ToErrorOr<GetSessionResponse>();
        }

        Session? session = await _sessionsRepository.GetByIdAsync(query.SessionId);
        if (session is null)
        {
            return Error
                .NotFound(description: "Session not found")
                .ToErrorOr<GetSessionResponse>();
        }

        return session
            .ToResponse()
            .ToErrorOr();
    }
}