using ErrorOr;
using MediatR;

namespace DddGym.Framework.BaseTypes.Application.Cqrs;

public interface ICommandUsecase<in TCommand> : IRequestHandler<TCommand, IErrorOr>
    where TCommand : ICommand;

public interface ICommandUsecase<in TCommand, TResponse> : IRequestHandler<TCommand, IErrorOr<TResponse>>
    where TCommand : ICommand<TResponse>
    where TResponse : IResponse;