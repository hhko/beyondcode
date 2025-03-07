using Microsoft.Extensions.DependencyInjection;

namespace Crop.Hello.Api.Adapters.Persistence.Abstractions.Registration;

public static class PersistenceLayerRegistration
{
    public static IServiceCollection RegisterPersistenceLayer(
        this IServiceCollection services)
    {
        return services;
    }
}