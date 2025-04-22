using LanguageExt;
using MediatR;

namespace DddGym.Framework.BaseTypes.Cqrs;

//public interface IQuery<out TResponse> : IRequest<IErrorOr<TResponse>>
//    where TResponse : IResponse;

public interface IQuery2<TResponse> : IRequest<Fin<TResponse>>
    where TResponse : IResponse;