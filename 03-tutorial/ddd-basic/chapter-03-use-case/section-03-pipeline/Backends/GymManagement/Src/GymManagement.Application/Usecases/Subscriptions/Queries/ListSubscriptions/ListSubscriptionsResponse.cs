using DddGym.Framework.BaseTypes.Application.Cqrs;
using GymManagement.Domain.AggregateRoots.Subscriptions;

namespace GymManagement.Application.Usecases.Subscriptions.Queries.ListSubscriptions;

public sealed record ListSubscriptionsResponse(
    List<Subscription> Subscriptions)
    : IResponse;