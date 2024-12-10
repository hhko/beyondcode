using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Crop.Hello.Master.Adapters.Infrastructure.Abstractions.Registration;

public static class PersistenceLayerRegistration
{
    public static IServiceCollection RegisterPersistenceLayer(this IServiceCollection services, IHostEnvironment environment, ILoggingBuilder logging)
    {
        services
            .RegisterOptions()
            .RegisterOpenTelemetry(logging, environment);

        return services;
    }

    //public static IApplicationBuilder UsePersistenceLayer(this IApplicationBuilder app)
    //{
    //    return app;
    //}
}