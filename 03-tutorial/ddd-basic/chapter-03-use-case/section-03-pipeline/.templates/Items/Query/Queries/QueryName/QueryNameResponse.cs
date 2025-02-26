using HostName.Application.Abstractions.BaseTypes.Cqrs;
using HostName.Domain.AggregateRoots.EntityNames;

namespace HostName.Application.Usecases.EntityNames.Queries.QueryName;

public sealed record QueryNameResponse()
    : IResponse;