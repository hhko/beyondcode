using MediatR;

namespace GymDdd.Framework.BaseTypes.Cqrs;

//public interface ICommand
//  : IRequest<IErrorOr>;
//public interface ICommand<out TResponse>
//  : IRequest<IErrorOr<TResponse>>
//    where TResponse : IResponse;

// 반환이 없는 것
//public interface ICommandUsecase<in TCommand>
//  : IRequestHandler<TCommand, IErrorOr>
//    where TCommand : ICommand;
// 반환이 있는 것
//public interface ICommandUsecase<in TCommand, TResponse>
//  : IRequestHandler<TCommand, IErrorOr<TResponse>>
//    where TCommand : ICommand<TResponse>
//    where TResponse : IResponse;

public interface ICommandReqeust<TResponse>
    : IRequest<Fin<TResponse>>
      where TResponse : IResponse;
