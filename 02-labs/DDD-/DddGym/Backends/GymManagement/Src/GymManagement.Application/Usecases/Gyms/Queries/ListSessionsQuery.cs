using FluentValidation;
using FunctionalDdd.Framework.BaseTypes.Cqrs;
using GymManagement.Domain.AggregateRoots.Sessions;
using GymManagement.Domain.AggregateRoots.Sessions.Enumerations;

namespace GymManagement.Application.Usecases.Gyms.Queries;

public static class ListSessionsQuery
{
    public sealed record Request(
        Guid GymId,
        DateTime? StartDateTime = null,
        DateTime? EndDateTime = null,
        List<SessionCategory>? Categories = null)
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
    //internal sealed class ListSessionsQueryUsecase
    //    : IQueryUsecase<ListSessionsQuery, ListSessionsResponse>
    //{
    //    private readonly ISessionsRepository _sessionsRepository;

    //    public ListSessionsQueryUsecase(ISessionsRepository sessionsRepository)
    //    {
    //        _sessionsRepository = sessionsRepository;
    //    }

    //    public async Task<IErrorOr<ListSessionsResponse>> Handle(ListSessionsQuery query, CancellationToken cancellationToken)
    //    {
    //        List<Session> sessions = await _sessionsRepository.ListByGymIdAsync(
    //            query.GymId,
    //            query.StartDateTime,
    //            query.EndDateTime,
    //            query.Categories);

    //        return sessions
    //            .ToResponse()
    //            .ToErrorOr();
    //    }
    //}
}