namespace GymDdd.Framework.BaseTypes.Cqrs;

public interface ICachedQuery
{
    string CacheKey { get; }
    TimeSpan? Duration { get; }
}

public interface ICachedQuery<TResponse>
    : IQueryRequest<TResponse>
    , ICachedQuery
      where TResponse : IResponse;