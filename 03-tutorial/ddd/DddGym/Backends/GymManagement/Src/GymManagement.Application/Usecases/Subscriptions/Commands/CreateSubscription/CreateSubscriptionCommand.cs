using DddGym.Framework.BaseTypes.Cqrs;
using GymManagement.Domain.AggregateRoots.Subscriptions.Enumerations;

namespace GymManagement.Application.Usecases.Subscriptions.Commands.CreateSubscription;

public sealed record CreateSubscriptionCommand(
    SubscriptionType SubscriptionType,
    Guid AddminId)
    : ICommand<CreateSubscriptionResponse>;