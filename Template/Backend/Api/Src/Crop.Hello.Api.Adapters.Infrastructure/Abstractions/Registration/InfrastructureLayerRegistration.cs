using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Crop.Hello.Api.Adapters.Infrastructure.Abstractions.Registration;

public static class InfrastructureLayerRegistration
{
    public static IServiceCollection RegisterInfrastructureLayer(
        this IServiceCollection services, 
        IHostEnvironment environment,
        IConfiguration configuration)
    {
        return services
            .RegisterOptions()
            .RegisterWindowsService()
            .RegisterOpenTelemetry(environment, configuration);
    }

    public static IHostBuilder EnalbeInfrastructureLayer(this IHostBuilder app)
    {
        return app
            .EnableWindowsService();
    }
}