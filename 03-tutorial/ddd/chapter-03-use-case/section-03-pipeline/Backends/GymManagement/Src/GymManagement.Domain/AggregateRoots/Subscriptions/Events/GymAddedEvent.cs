using DddGym.Framework.BaseTypes.Domain;
using GymManagement.Domain.AggregateRoots.Gyms;

namespace GymManagement.Domain.AggregateRoots.Subscriptions.Events;

public sealed record GymAddedEvent(
    Subscription Subscription,
    Gym Gym) : IDomainEvent;