using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

namespace Crop.Hello.Api.Adapters.Infrastructure.HealthChecks;

internal class HostedLifecycleService(
    HealthCheckService healthCheckService,
    ILogger<HostedLifecycleService> logger)
    : IHostedLifecycleService
{
    public Task StartAsync(CancellationToken cancellationToken) =>
         CheckHealthAsync(cancellationToken: cancellationToken);

    public Task StartedAsync(CancellationToken cancellationToken) =>
        CheckHealthAsync(cancellationToken: cancellationToken);

    public Task StartingAsync(CancellationToken cancellationToken) =>
        CheckHealthAsync(cancellationToken: cancellationToken);

    public Task StopAsync(CancellationToken cancellationToken) =>
        CheckHealthAsync(cancellationToken: cancellationToken);

    public Task StoppedAsync(CancellationToken cancellationToken) =>
        CheckHealthAsync(cancellationToken: cancellationToken);

    public Task StoppingAsync(CancellationToken cancellationToken) =>
        CheckHealthAsync(cancellationToken: cancellationToken);

    public Task ReadyAsync() =>
        CheckHealthAsync();

    private async Task CheckHealthAsync(
         [CallerMemberName] string eventName = "",
         CancellationToken cancellationToken = default)
    {
        HealthReport result =
            await healthCheckService.CheckHealthAsync(cancellationToken);

        logger.LogInformation(
            "{EventName}: {Status}", eventName, result.Status);
    }
}
