using FunctionalDdd.Framework.BaseTypes.Cqrs;
using GymManagement.Domain.AggregateRoots.Subscriptions;

namespace GymManagement.Application.Usecases.Subscriptions.Commands.CreateSubscription;

public sealed record CreateSubscriptionResponse(
    Subscription Subscription)
    : IResponse;