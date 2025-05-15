using SolutionName.Framework.BaseTypes.Application.Cqrs;
using HostName.Domain.AggregateRoots.EntityNames;
using ErrorOr;

namespace HostName.Application.Usecases.EntityNames.Queries.QueryName;

internal sealed class QueryNameQueryUsecase
    : IQueryUsecase<QueryNameQuery, QueryNameResponse>
{
    public async Task<IErrorOr<QueryNameResponse>> Handle(QueryNameQuery query, CancellationToken cancellationToken)
    {
        // return Error
        //     .NotFound(description: "Subscription not found")
        //     .ToErrorOr<CommandNameResponse>();

        // return xyx
        //     .ToResponse()
        //     .ToErrorOr();
    }
}
