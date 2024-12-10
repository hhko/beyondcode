using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Crop.Hello.Api.Adapters.Infrastructure.Abstractions.Registration;

public static class PersistenceLayerRegistration
{
    public static IServiceCollection RegisterPersistenceLayer(
        this IServiceCollection services, 
        IHostEnvironment environment, 
        ILoggingBuilder logging,
        IConfigurationManager configuration)
    {
        services
            .RegisterOptions()
            .RegisterOpenTelemetry(logging, environment, configuration);

        return services;
    }

    //public static IApplicationBuilder UsePersistenceLayer(this IApplicationBuilder app)
    //{
    //    return app;
    //}
}