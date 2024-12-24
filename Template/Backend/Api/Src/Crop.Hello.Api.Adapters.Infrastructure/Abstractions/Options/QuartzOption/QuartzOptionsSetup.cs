using Crop.Hello.Api.Adapters.Infrastructure.Jobs;
using Microsoft.Extensions.Options;
using Quartz;

namespace Crop.Hello.Api.Adapters.Infrastructure.Abstractions.Options.QuartzOption;

internal sealed class QuartzOptionsSetup(
    IOptions<JobOptions> workerOptions)
    : IConfigureOptions<QuartzOptions>
{
    private readonly IOptions<JobOptions> _workerOptions = workerOptions;

    public void Configure(QuartzOptions options)
    {
        var jobKey = JobKey.Create(nameof(ProcessOutboxMessagesJob));

        options
            .AddJob<ProcessOutboxMessagesJob>(jobBuilder =>
                jobBuilder
                    .WithIdentity(jobKey))
            .AddTrigger(trigger =>
                trigger
                    .ForJob(jobKey)
                    .StartNow()
                    .WithIdentity(nameof(ProcessOutboxMessagesJob) + "-Trigger")
                    .WithCronSchedule(_workerOptions.Value.CronSchedule));
    }
}

