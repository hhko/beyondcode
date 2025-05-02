using GymManagement.Application.Usecases.Participants.Queries;
using GymManagement.Domain.AggregateRoots.Sessions;

namespace GymManagement.Application.Usecases.Participants;

internal static class ParticipantMappings
{
    public static ListParticipantSessionsQuery.Response ToListParticipantSessionsResponse(this List<Session> sessions)
    {
        return new ListParticipantSessionsQuery.Response(sessions);
    }
}
