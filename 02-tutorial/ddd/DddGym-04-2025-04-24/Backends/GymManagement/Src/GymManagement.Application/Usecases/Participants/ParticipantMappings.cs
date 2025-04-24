using GymManagement.Application.Usecases.Participants.Queries.ListParticipantSessions;
using GymManagement.Domain.AggregateRoots.Sessions;

namespace GymManagement.Application.Usecases.Participants;

internal static class ParticipantMappings
{
    public static ListParticipantSessionsResponse ToResponse(this List<Session> sessions)
    {
        return new ListParticipantSessionsResponse(sessions);
    }
}
