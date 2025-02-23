using DddGym.Domain.AggregateRoots.Subscriptions.Enumerations;
using DddGym.Domain.AggregateRoots.Subscriptions;
using ErrorOr;
using MediatR;

namespace DddGym.Application.Abstractions.BaseTypes.Cqrs;

//public interface ICommandusecase<in TCommand> : IRequestHandler<TCommand, ErrorOr>
//    where TCommand : ICommand
//{
//}

public interface ICommandusecase<in TCommand, TResponse> : IRequestHandler<TCommand, IErrorOr<TResponse>>
    where TCommand : ICommand<TResponse>
    where TResponse : IResponse
{
}

//public record CreateSubscriptionCommand(SubscriptionType SubscriptionType, Guid AdminId)
//    : IRequest<ErrorOr<Subscription>>;