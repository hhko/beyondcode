using LanguageExt;
using MediatR;

namespace FunctionalDdd.Framework.BaseTypes.Cqrs;

// public interface IQuery<out TResponse>               <-- out     : x
//  : IRequest<IResult<TResponse>>                      <-- IResult : Fin
//    where TResponse : IResponse;

public interface IQuery2<TResponse>
    : IRequest<Fin<TResponse>>
      where TResponse : IResponse;

//public interface IQueryHandler<in TQuery, TResponse>
//  : IRequestHandler<TQuery, IResult<TResponse>>       <-- IResult: Fin
//    where TQuery : IQuery<TResponse>
//    where TResponse : IResponse
//{
//}

public interface IQueryUsecase2<in TQuery, TResponse>
    : IRequestHandler<TQuery, Fin<TResponse>>
      where TQuery : IQuery2<TResponse>
      where TResponse : IResponse;