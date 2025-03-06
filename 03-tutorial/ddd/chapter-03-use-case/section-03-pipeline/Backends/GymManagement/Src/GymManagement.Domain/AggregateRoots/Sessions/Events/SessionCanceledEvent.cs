using DddGym.Framework.BaseTypes.Domain;

namespace GymManagement.Domain.AggregateRoots.Sessions.Events;

public sealed record SessionCanceledEvent(
    Session Session) : IDomainEvent;