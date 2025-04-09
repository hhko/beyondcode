using LanguageExt;
using MediatR;

namespace DddGym.Framework.BaseTypes.Cqrs;

//public interface IQueryUsecase<in TQuery, TResponse> : IRequestHandler<TQuery, IErrorOr<TResponse>>
//    where TQuery : IQuery<TResponse>
//    where TResponse : IResponse;


public interface IQueryUsecase2<in TQuery, TResponse> : IRequestHandler<TQuery, Fin<TResponse>>
    where TQuery : IQuery2<TResponse>
    where TResponse : IResponse;