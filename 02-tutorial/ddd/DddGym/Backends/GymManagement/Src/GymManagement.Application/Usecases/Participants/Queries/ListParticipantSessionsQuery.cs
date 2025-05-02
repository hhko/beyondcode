using FluentValidation;
using FunctionalDdd.Framework.BaseTypes.Cqrs;
using GymManagement.Domain.AggregateRoots.Sessions;

namespace GymManagement.Application.Usecases.Participants.Queries;

public static class ListParticipantSessionsQuery
{
    public sealed record Request(
        Guid ParticipantId,
        DateTime? StartDateTime = null,
        DateTime? EndDateTime = null)
        : IQuery<Response>;

    public sealed record Response(
        List<Session> Sessions)
        : IResponse;

    internal sealed class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
        }
    }

    //// TODO: LanguageExt
    //internal sealed class ListParticipantSessionsQueryUsecase
    //    : IQueryUsecase<ListParticipantSessionsQuery, ListParticipantSessionsResponse>
    //{
    //    private readonly ISessionsRepository _sessionsRepository;
    //    private readonly IParticipantsRepository _participantsRepository;

    //    public ListParticipantSessionsQueryUsecase(
    //        ISessionsRepository sessionsRepository,
    //        IParticipantsRepository participantsRepository)
    //    {
    //        _sessionsRepository = sessionsRepository;
    //        _participantsRepository = participantsRepository;
    //    }

    //    public async Task<IErrorOr<ListParticipantSessionsResponse>> Handle(ListParticipantSessionsQuery query, CancellationToken cancellationToken)
    //    {
    //        Participant? participant = await _participantsRepository.GetByIdAsync(query.ParticipantId);
    //        if (participant is null)
    //        {
    //            return Error
    //                .NotFound(description: "Participant not found")
    //                .ToErrorOr<ListParticipantSessionsResponse>();
    //        }

    //        List<Session> sessions = await _sessionsRepository.ListByIds(participant.SessionIds, query.StartDateTime, query.EndDateTime);

    //        return sessions
    //            .ToResponse()
    //            .ToErrorOr();
    //    }
    //}
}