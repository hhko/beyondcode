using Quartz;

namespace Crop.Hello.Api.Adapters.Infrastructure.Jobs;

internal sealed class ProcessOutboxMessagesJob
    : IJob
{
    public Task Execute(IJobExecutionContext context)
    {
        return Task.CompletedTask;
    }
}
