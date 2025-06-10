using Microsoft.Extensions.DependencyInjection;

namespace GymManagement.Adapters.Persistence.Abstractions.Registrations;

public static class AdapterPersistenceRegistration
{
    public static IServiceCollection RegisterAdapterPersistence(this IServiceCollection services)
    {
        services.RegisterRepository();

        return services;
    }
}
