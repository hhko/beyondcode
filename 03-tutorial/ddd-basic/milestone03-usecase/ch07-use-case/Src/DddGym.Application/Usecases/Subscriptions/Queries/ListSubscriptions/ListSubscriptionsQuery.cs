using DddGym.Application.Abstractions.BaseTypes.Cqrs;

namespace DddGym.Application.Usecases.Subscriptions.Queries.ListSubscriptions;

public sealed record ListSubscriptionsQuery(string Name) 
    : IQuery<SubscriptionsResponse>;


