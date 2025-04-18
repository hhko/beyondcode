using DddGym.Framework.BaseTypes.Cqrs;

namespace GymManagement.Application.Usecases.Sessions.Queries.GetSession;

public sealed record GetSessionQuery(
    Guid RoomId,
    Guid SessionId) : IQuery2<GetSessionResponse>;