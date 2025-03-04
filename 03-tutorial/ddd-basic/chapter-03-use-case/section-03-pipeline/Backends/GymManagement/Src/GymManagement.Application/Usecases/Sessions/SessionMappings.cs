using GymManagement.Application.Usecases.Sessions.Commands.CreateSession;
using GymManagement.Application.Usecases.Sessions.Queries.GetSession;
using GymManagement.Domain.AggregateRoots.Sessions;

namespace GymManagement.Application.Usecases.Sessions;

internal static class SessionMappings
{
    public static GetSessionResponse ToResponse(this Session session)
    {
        return new GetSessionResponse(session);
    }

    public static CreateSessionResponse ToResponseCreated(this Session session)
    {
        return new CreateSessionResponse(session);
    }
}
