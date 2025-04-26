using FluentValidation;
using FunctionalDdd.Framework.BaseTypes.Cqrs;
using LanguageExt;
using MediatR;
using Microsoft.Extensions.Logging;
using ZiggyCreatures.Caching.Fusion;

namespace FunctionalDdd.Framework.Pipelines;

// 로그
//public sealed class LoggingPipeline<TRequest, TResponse>(ILogger<LoggingPipeline<TRequest, TResponse>> logger)
//  : IPipelineBehavior<TRequest, TResponse>
//    where TRequest : IRequest<TResponse>
//    where TResponse : IResult

// 유효성 검사
//public sealed class FluentValidationPipeline<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
//  : IPipelineBehavior<TRequest, TResponse>
//    where TRequest : IRequest<TResponse>
//    where TResponse : class, IResult

// 캐시
//public sealed class QueryCachingPipeline<TRequest, TResponse>(IFusionCache fusionCache)
//  : IPipelineBehavior<TRequest, TResponse>
//    where TRequest : class, IRequest<TResponse>, IQuery<IResponse>, ICachedQuery
//    where TResponse : IResult<IResponse>

//public interface IQuery<out TResponse>
//  : IRequest<IResult<TResponse>>
//    where TResponse : IResponse


//public interface IQuery2<TResponse>
//    : IRequest<Fin<TResponse>>
//      where TResponse : IResponse;

// TResponse를 Fin<TInnerResponse>로 고정하는 패턴이야.
//public sealed class QueryCachingPipeline<TQuery, TInnerResponse>
//    : IPipelineBehavior<TQuery, Fin<TInnerResponse>>
//    where TQuery : class, IRequest<Fin<TInnerResponse>>, ICachedQuery
//    where TInnerResponse : class, IResponse
//public async Task<Fin<TInnerResponse>> Handle(TQuery query, RequestHandlerDelegate<Fin<TInnerResponse>> next, CancellationToken cancellationToken)
//{
//    var response = await next();
//    // 여기서 바로 Fin<TInnerResponse> 다루면 됨
//    return response;
//}

//public sealed class QueryCachingPipeline<TQuery, TResponse>
//    : IPipelineBehavior<TQuery, Fin<TResponse>> // 그대로 유지
//    where TQuery : class, IRequest<Fin<TResponse>>, ICachedQuery
//    where TResponse : class, IResponse


//public sealed class QueryCachingPipeline<TQuery, TResponse>
//    : IPipelineBehavior<TQuery, TResponse>
//      where TQuery : class, IRequest<TResponse>, ICachedQuery //IQuery2<IResponse>, ICachedQuery
//      where TResponse : class, IResponse //, Fin<IResponse>
//{
//    //private readonly IFusionCache _fusionCache = fusionCache;
//    //private readonly IFusionCache _fusionCache;

//    public async Task<TResponse> Handle(TQuery request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
//    {
//        TResponse result = await next();


//        return result;
//    }

//    //public async Task<TResponse> Handle(TQuery query, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
//    //{
//    //    //var cachedResult = await _fusionCache.GetOrDefaultAsync<TResponse>(query.CacheKey, token: cancellationToken);

//    //    //if (cachedResult is not null)
//    //    //{
//    //    //    return cachedResult;
//    //    //}

//    //    var result = await next();



//    //    //if (result.IsSucc)
//    //    //{
//    //    //    await _fusionCache.SetAsync
//    //    //    (
//    //    //        query.CacheKey,
//    //    //        result,
//    //    //        query.Duration ?? CacheDuration.Default,
//    //    //        token: cancellationToken
//    //    //    );
//    //    //}

//    //    return result;

//    //    //var result = await next();
//    //    //return result;
//    //}


//}

//-----------------------------------------
// 성공

//public interface IQuery2<TResponse>
//    : IRequest<Fin<TResponse>>
//      where TResponse : IResponse;

// 원본
//public sealed class QueryCachingPipeline<TRequest, TResponse>(IFusionCache fusionCache)
//  : IPipelineBehavior<TRequest, TResponse>
//    where TRequest : class, IRequest<TResponse>, IQuery<IResponse>, ICachedQuery
//    where TResponse : IResult<IResponse>

public sealed class QueryCachingPipeline<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
      where TRequest : class, IRequest<TResponse>, ICachedQuery //IQuery2<IResponse>, ICachedQuery
      where TResponse : class //, Fin<IResponse>
{
    //private readonly IFusionCache _fusionCache = fusionCache;
    //private readonly IFusionCache _fusionCache;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        //var cachedResult = await _fusionCache.GetOrDefaultAsync<TResponse>(query.CacheKey, token: cancellationToken);

        //if (cachedResult is not null)
        //{
        //    return cachedResult;
        //}

        //
        // TResponse    : Response 클래스

        var result = await next();

        //if (result is Fin<TResponse> fin)
        //{
        //    if (fin.IsSucc)
        //    {

        //    }
        //}

        //Fin? fin2 = result as Fin;


        //if (result.IsSucc)
        //{
        //    await _fusionCache.SetAsync
        //    (
        //        query.CacheKey,
        //        result,
        //        query.Duration ?? CacheDuration.Default,
        //        token: cancellationToken
        //    );
        //}

        return result;

        //var result = await next();
        //return result;
    }
}

//public interface IQueryUsecase2<in TQuery, TResponse>
//    : IRequestHandler<TQuery, Fin<TResponse>>
//      where TQuery : IQuery2<TResponse>
//      where TResponse : IResponse;

public sealed class QueryCachingPipeline2<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
      where TRequest : class, IRequest<TResponse>, ICachedQuery //, IQuery2<IResponse> //, ICachedQuery
      where TResponse : Fin<IResponse>
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var result = await next();
        return result;
    }
}

file static class CacheDuration
{
    internal static TimeSpan Default = TimeSpan.FromMinutes(2);
}
