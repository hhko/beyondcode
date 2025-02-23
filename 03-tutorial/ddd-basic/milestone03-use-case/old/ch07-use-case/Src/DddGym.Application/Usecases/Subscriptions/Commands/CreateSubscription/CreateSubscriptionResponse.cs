using DddGym.Application.Abstractions.BaseTypes.Cqrs;
using DddGym.Domain.AggregateRoots.Subscriptions;

namespace DddGym.Application.Usecases.Subscriptions.Commands.CreateSubscription;

public sealed record CreateSubscriptionResponse(
    Subscription Subscription)
    : IResponse;