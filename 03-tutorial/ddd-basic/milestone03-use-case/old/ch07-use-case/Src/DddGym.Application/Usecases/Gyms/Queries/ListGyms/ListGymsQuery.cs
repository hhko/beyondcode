using DddGym.Application.Abstractions.BaseTypes.Cqrs;

namespace DddGym.Application.Usecases.Gyms.Queries.ListGyms;

public sealed record ListGymsQuery(
    Guid SubscriptionId)
    : IQuery<ListGymsResponse>;
