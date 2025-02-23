using DddGym.Application.Abstractions.BaseTypes.Cqrs;
using DddGym.Domain.AggregateRoots.Subscriptions.Enumerations;

namespace DddGym.Application.Usecases.Subscriptions.Commands.CreateSubscription;

public sealed record CreateSubscriptionCommand(
    SubscriptionType SubscriptionType,
    Guid AddminId)
    : ICommand<CreateSubscriptionResponse>;