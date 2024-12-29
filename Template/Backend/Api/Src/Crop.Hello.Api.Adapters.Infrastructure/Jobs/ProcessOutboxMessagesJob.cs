using Crop.Hello.Api.Adapters.Infrastructure.Abstractions.Options.OpenTelemetry;
using Crop.Hello.Api.Adapters.Infrastructure.Abstractions.Options.Quartz;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Quartz;

namespace Crop.Hello.Api.Adapters.Infrastructure.Jobs;

internal sealed class ProcessOutboxMessagesJob(
    ILogger<ProcessOutboxMessagesJob> logger)
    : IJob
{
    private readonly ILogger _logger = logger;

    public Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("{Key} is {Value}", "ProcessOutboxMessagesJob", "월요일");

        return Task.CompletedTask;
    }
}
