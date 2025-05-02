namespace FunctionalDdd.Framework.BaseTypes.Cqrs;

public interface ICachedQuery
{
    string CacheKey { get; }
    TimeSpan? Duration { get; }
}

public interface ICachedQuery<TResponse>
    : IQuery<TResponse>
    , ICachedQuery
      where TResponse : IResponse;