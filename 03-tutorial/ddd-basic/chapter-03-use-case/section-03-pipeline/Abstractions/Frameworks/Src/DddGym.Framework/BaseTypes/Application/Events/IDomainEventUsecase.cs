using DddGym.Framework.BaseTypes.Domain;
using MediatR;

namespace DddGym.Framework.BaseTypes.Application.Events;

public interface IDomainEventUsecase<in TEvent>
    : INotificationHandler<TEvent>
    where TEvent : IDomainEvent
{
}