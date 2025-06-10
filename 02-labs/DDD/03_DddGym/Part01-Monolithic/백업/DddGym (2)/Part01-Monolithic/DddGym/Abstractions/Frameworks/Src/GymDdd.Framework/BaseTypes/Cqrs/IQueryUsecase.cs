using MediatR;

namespace GymDdd.Framework.BaseTypes.Cqrs;

//public interface IQueryHandler<in TQuery, TResponse>
//  : IRequestHandler<TQuery, IResult<TResponse>>       <-- IResult: Fin
//    where TQuery : IQuery<TResponse>
//    where TResponse : IResponse
//{
//}

public interface IQueryUsecase<in TQuery, TResponse>
    : IRequestHandler<TQuery, Fin<TResponse>>
      where TQuery : IQueryRequest<TResponse>
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