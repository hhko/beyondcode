using ErrorOr;
using LanguageExt;
using MediatR;

namespace DddGym.Framework.BaseTypes.Cqrs;

public interface ICommandUsecase<in TCommand> : IRequestHandler<TCommand, IErrorOr>
    where TCommand : ICommand;

public interface ICommandUsecase<in TCommand, TResponse> : IRequestHandler<TCommand, IErrorOr<TResponse>>
    where TCommand : ICommand<TResponse>
    where TResponse : IResponse;

// CS1961
// Invalid variance:
//  The type parameter 'TResponse' must be invariantly valid on 'IRequest<Fin<TResponse>>'.
//  'TResponse' is contravariant.
public interface ICommand2<TResponse> : IRequest<Fin<TResponse>>
    where TResponse : IResponse;

public interface ICommandUsecase2<in TCommand, TResponse> : IRequestHandler<TCommand, Fin<TResponse>>
    where TCommand : ICommand2<TResponse>
    where TResponse : IResponse;