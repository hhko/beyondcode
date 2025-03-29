using DddGym.Framework.BaseTypes.Events;

namespace GymManagement.Domain.AggregateRoots.Sessions.Events;

public sealed record SessionCanceledEvent(
    Session Session) : IDomainEvent;