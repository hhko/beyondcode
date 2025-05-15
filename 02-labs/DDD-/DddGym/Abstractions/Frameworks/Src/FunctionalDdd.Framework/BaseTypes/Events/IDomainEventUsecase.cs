using MediatR;

namespace FunctionalDdd.Framework.BaseTypes.Events;

public interface IDomainEventUsecase<in TEvent>
    : INotificationHandler<TEvent>
    where TEvent : IDomainEvent
{
}