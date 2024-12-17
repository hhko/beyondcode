using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

namespace Crop.Hello.Api.Adapters.Infrastructure.HealthChecks;

// https://learn.microsoft.com/en-us/dotnet/core/diagnostics/diagnostic-health-checks
// https://medium.com/@jeslurrahman/implementing-health-checks-in-net-8-c3ba10af83c3
// https://weblogs.asp.net/ricardoperes/checking-the-heath-of-an-asp-net-core-application
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
