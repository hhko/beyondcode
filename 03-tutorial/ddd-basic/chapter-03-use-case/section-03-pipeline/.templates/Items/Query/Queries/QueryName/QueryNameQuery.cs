using HostName.Application.Abstractions.BaseTypes.Cqrs;

namespace HostName.Application.Usecases.EntityNames.Queries.QueryName;

public sealed record QueryNameQuery()
    : IQuery<QueryNameResponse>;