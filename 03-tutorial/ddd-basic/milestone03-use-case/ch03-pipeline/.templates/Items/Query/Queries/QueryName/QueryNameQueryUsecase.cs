using HostName.Application.Abstractions.BaseTypes.Cqrs;
using HostName.Domain.AggregateRoots.EntityNames;
using ErrorOr;

namespace HostName.Application.Usecases.EntityNames.Queries.QueryName;

internal sealed class QueryNameQueryUsecase
    : IQueryUsecase<QueryNameQuery, QueryNameResponse>
{
    public async Task<IErrorOr<QueryNameResponse>> Handle(QueryNameQuery query, CancellationToken cancellationToken)
    {
        // var subscriptions = await _subscriptionRepository.ListAsync();

        // return subscriptions
        //     .ToResponse()
        //     .ToErrorOr();
    }
}
