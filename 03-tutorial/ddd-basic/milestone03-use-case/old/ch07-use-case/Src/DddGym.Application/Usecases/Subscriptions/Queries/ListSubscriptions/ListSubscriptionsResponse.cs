using DddGym.Application.Abstractions.BaseTypes.Cqrs;
using DddGym.Domain.AggregateRoots.Subscriptions;

namespace DddGym.Application.Usecases.Subscriptions.Queries.ListSubscriptions;

public sealed record ListSubscriptionsResponse(
    List<Subscription> Subscriptions) 
    : IResponse;