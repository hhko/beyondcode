using DddGym.Framework.BaseTypes.Cqrs;
using ErrorOr;
using GymManagement.Application.Abstractions.Repositories;
using GymManagement.Domain.AggregateRoots.Participants;
using GymManagement.Domain.AggregateRoots.Sessions;

namespace GymManagement.Application.Usecases.Participants.Queries.ListParticipantSessions;

internal sealed class ListParticipantSessionsQueryUsecase
    : IQueryUsecase<ListParticipantSessionsQuery, ListParticipantSessionsResponse>
{
    private readonly ISessionsRepository _sessionsRepository;
    private readonly IParticipantsRepository _participantsRepository;

    public ListParticipantSessionsQueryUsecase(
        ISessionsRepository sessionsRepository,
        IParticipantsRepository participantsRepository)
    {
        _sessionsRepository = sessionsRepository;
        _participantsRepository = participantsRepository;
    }

    public async Task<IErrorOr<ListParticipantSessionsResponse>> Handle(ListParticipantSessionsQuery query, CancellationToken cancellationToken)
    {
        Participant? participant = await _participantsRepository.GetByIdAsync(query.ParticipantId);
        if (participant is null)
        {
            return Error
                .NotFound(description: "Participant not found")
                .ToErrorOr<ListParticipantSessionsResponse>();
        }

        List<Session> sessions = await _sessionsRepository.ListByIds(participant.SessionIds, query.StartDateTime, query.EndDateTime);

        return sessions
            .ToResponse()
            .ToErrorOr();
    }
}
