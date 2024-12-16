using Microsoft.Extensions.DependencyInjection;

namespace Crop.Hello.Api.Adapters.Infrastructure.Abstractions.Registration;

internal static class HealthChecksRegistration
{
    internal static IServiceCollection RegisterHealthChecks(
        this IServiceCollection services)
    {
        services
            .AddHealthChecks()
            .AddResourceUtilizationHealthCheck();
        
        return services;
    }
}
