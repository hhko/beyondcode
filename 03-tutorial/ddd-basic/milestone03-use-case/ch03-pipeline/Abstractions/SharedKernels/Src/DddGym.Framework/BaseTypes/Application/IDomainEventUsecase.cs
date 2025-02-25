using DddGym.Framework.BaseTypes.Domain;
using MediatR;

namespace DddGym.Framework.BaseTypes.Application;

public interface IDomainEventUsecase<in TEvent>
    : INotificationHandler<TEvent>
    where TEvent : IDomainEvent
{
}