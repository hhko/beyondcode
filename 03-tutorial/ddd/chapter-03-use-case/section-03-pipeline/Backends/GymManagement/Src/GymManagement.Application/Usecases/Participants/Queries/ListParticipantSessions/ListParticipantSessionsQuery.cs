using DddGym.Framework.BaseTypes.Application.Cqrs;

namespace GymManagement.Application.Usecases.Participants.Queries.ListParticipantSessions;

public sealed record ListParticipantSessionsQuery(
    Guid ParticipantId,
    DateTime? StartDateTime = null,
    DateTime? EndDateTime = null)
    : IQuery<ListParticipantSessionsResponse>;