using Crop.Hello.Api.Adapters.Infrastructure.Abstractions.Options.OpenTelemetry;
using Crop.Hello.Api.Adapters.Infrastructure.Abstractions.Options.Quartz;
using Microsoft.Extensions.Options;
using Quartz;

namespace Crop.Hello.Api.Adapters.Infrastructure.Jobs;

internal sealed class ProcessOutboxMessagesJob
    : IJob
{
    public ProcessOutboxMessagesJob(
        IOptions<OpenTelemetryOptions> openTelemetryOptions,
        IOptions<JobOptions> jobOptions)
    {
        string teamName1 = openTelemetryOptions.Value.TeamName;
        string teamName2 = openTelemetryOptions.Value.TeamName;

        string cronSchedule1 = jobOptions.Value.CronSchedule;
        string cronSchedule2 = jobOptions.Value.CronSchedule;
    }

    public Task Execute(IJobExecutionContext context)
    {
        return Task.CompletedTask;
    }
}
