using MediatR;

namespace DddGym.Framework.BaseTypes.Events;

public interface IDomainEventUsecase<in TEvent>
    : INotificationHandler<TEvent>
    where TEvent : IDomainEvent
{
}