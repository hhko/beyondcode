using GymManagement.Application.Usecases.Sessions.Commands;
using GymManagement.Application.Usecases.Sessions.Queries;
using GymManagement.Domain.AggregateRoots.Sessions;

namespace GymManagement.Application.Usecases.Sessions;

internal static class SessionMappings
{
    public static GetSessionQuery.Response ToGetSessionResponse(this Session session)
    {
        return new GetSessionQuery.Response(session);
    }

    public static CreateSessionCommand.Response ToCreateSessionResponse(this Session session)
    {
        return new CreateSessionCommand.Response(session);
    }
}
