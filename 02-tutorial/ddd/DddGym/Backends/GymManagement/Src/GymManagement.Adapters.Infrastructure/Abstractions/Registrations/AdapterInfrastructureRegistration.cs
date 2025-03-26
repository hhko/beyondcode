using Microsoft.Extensions.DependencyInjection;

namespace GymManagement.Adapters.Infrastructure.Abstractions.Registrations;

public static class AdapterInfrastructureRegistration
{
    public static IServiceCollection RegisterAdapterInfrastructure(this IServiceCollection services)
    {
        services.RegisterSystem();
        services.RegisterAuthentication();

        return services;
    }
}