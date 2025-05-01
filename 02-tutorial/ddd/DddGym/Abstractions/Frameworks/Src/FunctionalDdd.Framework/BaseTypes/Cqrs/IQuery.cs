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


//public interface IQuery3<out TResponse>
//    : IRequest<IResponse3>
//      where TResponse : IResponse3;

//public interface IResponse3
//{

//}

//public interface IQueryUsecase3<in TRequest, TResponse>
//    : IRequestHandler<TRequest, TResponse>
//      where TRequest : IQuery3<TResponse>
//      where TResponse : IResponse3;


//public interface IResponse3<T>
//{
//    Fin<T> Result { get; init; }
//}

//public interface IQuery3<out TResponse> 
//    : IRequest<TResponse> 
//      where TResponse : IResponse3<T>;

//public interface IQueryUsecase3<in TRequest, TResponse>
//    : IRequestHandler<TRequest, TResponse>
//      where TRequest : IQuery3<TResponse>
//      where TResponse : IResponse3;


//public interface IRequestHandler<in TRequest, TResponse>
//    where TRequest : IRequest<TResponse>