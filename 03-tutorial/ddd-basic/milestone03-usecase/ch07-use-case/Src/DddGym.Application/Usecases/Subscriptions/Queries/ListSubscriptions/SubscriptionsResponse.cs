using DddGym.Application.Abstractions.BaseTypes.Cqrs;
using DddGym.Domain.AggregateRoots.Subscriptions;

namespace DddGym.Application.Usecases.Subscriptions.Queries.ListSubscriptions;

public sealed record SubscriptionsResponse(List<Subscription> Subscriptions) 
    : IResponse;