using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GymManagement.Application.Abstractions.Pipelines;

public sealed class LoggingPipeline<TRequest, TResponse>(ILogger<LoggingPipeline<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : IErrorOr
{
    private readonly ILogger<LoggingPipeline<TRequest, TResponse>> _logger = logger;

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        _logger.LogStartingRequest(typeof(TRequest).Name, DateTime.UtcNow);

        var result = await next();

        if (!result.IsError)
        {
            _logger.LogSucceededRequest(typeof(TRequest).Name, DateTime.UtcNow);
        }
        else
        {
            _logger.LogFailedRequest(typeof(TRequest).Name, result.Errors!, DateTime.UtcNow);
        }


        //if (result is IValidationResult validationResult)
        //{
        //    _logger.LogFailedRequestBasedOnValidationErrors(typeof(TRequest).Name, validationResult.ValidationErrors, DateTime.UtcNow);
        //    return result;
        //}

        //_logger.LogFailedRequestBasedOnSingleError(typeof(TRequest).Name, result.Error, DateTime.UtcNow);
        return result;
    }
}

public static partial class LoggerMessageDefinitionsUtilities
{
    [LoggerMessage
    (
        //EventId = 1,

        // TODO: EventName 역할
        //EventName = $"StartingRequest in {nameof(LoggingPipeline<IRequest<IErrorOr>, IErrorOr>)}",
        Level = LogLevel.Information,
        Message = "Starting request {RequestName}, {DateTimeUtc}",
        SkipEnabledCheck = false
    )]
    public static partial void LogStartingRequest(this ILogger logger, string requestName, DateTime dateTimeUtc);

    [LoggerMessage
    (
        //EventId = 2,
        //EventName = $"CompletingRequest in {nameof(LoggingPipeline<IRequest<IErrorOr>, IErrorOr>)}",
        Level = LogLevel.Information,
        Message = "Request completed {requestName}, {DateTimeUtc}",
        SkipEnabledCheck = false
    )]
    public static partial void LogSucceededRequest(this ILogger logger, string requestName, DateTime dateTimeUtc);

    [LoggerMessage
    (
        //EventId = 3,
        //EventName = $"FailedRequestBasedOnSingleError in {nameof(LoggingPipeline<IRequest<IErrorOr>, IErrorOr>)}",
        Level = LogLevel.Error,
        Message = "Request failed {RequestName}, {Errors}, {DateTimeUtc}",
        SkipEnabledCheck = true
    )]
    public static partial void LogFailedRequest(this ILogger logger, string requestName, List<Error> errors, DateTime dateTimeUtc);
}
