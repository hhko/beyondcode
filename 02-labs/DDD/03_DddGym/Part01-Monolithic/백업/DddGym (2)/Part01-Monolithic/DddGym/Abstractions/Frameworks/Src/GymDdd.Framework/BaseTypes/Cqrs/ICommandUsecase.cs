using MediatR;

namespace GymDdd.Framework.BaseTypes.Cqrs;

// 반환이 없는 것
//public interface ICommandUsecase2<in TCommand> : IRequestHandler<TCommand, IErrorOr>
//    where TCommand : ICommand2;

// 반환이 있는 것
public interface ICommandUsecase<in TCommand, TResponse>
    : IRequestHandler<TCommand, Fin<TResponse>>
      where TCommand : ICommandReqeust<TResponse>
      where TResponse : IResponse;