using DddGym.Framework.BaseTypes.Application.Cqrs;
using GymManagement.Domain.AggregateRoots.Subscriptions;

namespace GymManagement.Application.Usecases.Subscriptions.Commands.CreateSubscription;

public sealed record CreateSubscriptionResponse(
    Subscription Subscription)
    : IResponse;