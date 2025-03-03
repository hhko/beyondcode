using DddGym.Framework.BaseTypes.Application.Cqrs;

namespace GymManagement.Application.Usecases.Sessions.Queries.GetSession;

public sealed record GetSessionQuery(
    Guid RoomId,
    Guid SessionId) : IQuery<GetSessionResponse>;