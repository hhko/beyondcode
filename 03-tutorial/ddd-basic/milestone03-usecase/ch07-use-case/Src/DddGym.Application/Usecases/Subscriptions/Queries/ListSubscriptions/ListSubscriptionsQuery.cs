using DddGym.Application.Abstractions.BaseTypes.Cqrs;

namespace DddGym.Application.Usecases.Subscriptions.Queries.ListSubscriptions;

public sealed record ListSubscriptionsQuery() 
    : IQuery<SubscriptionsResponse>;


