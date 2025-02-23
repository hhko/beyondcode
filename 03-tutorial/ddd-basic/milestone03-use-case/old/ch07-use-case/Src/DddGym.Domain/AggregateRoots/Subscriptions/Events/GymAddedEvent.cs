using DddGym.Domain.Abstractions.BaseTypes;
using DddGym.Domain.AggregateRoots.Gyms;

namespace DddGym.Domain.AggregateRoots.Subscriptions.Events;

public sealed record GymAddedEvent(
    Subscription Subscription,
    Gym Gym) : IDomainEvent;
