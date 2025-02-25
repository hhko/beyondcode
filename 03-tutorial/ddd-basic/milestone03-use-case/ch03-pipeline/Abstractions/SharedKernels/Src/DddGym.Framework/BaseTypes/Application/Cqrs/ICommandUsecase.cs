using ErrorOr;
using MediatR;

namespace DddGym.Framework.BaseTypes.Application.Cqrs;

public interface ICommandusecase<in TCommand, TResponse> : IRequestHandler<TCommand, IErrorOr<TResponse>>
    where TCommand : ICommand<TResponse>
    where TResponse : IResponse
{
}