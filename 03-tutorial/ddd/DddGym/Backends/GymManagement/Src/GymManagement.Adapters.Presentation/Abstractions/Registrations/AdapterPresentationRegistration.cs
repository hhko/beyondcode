using Microsoft.Extensions.DependencyInjection;

namespace GymManagement.Adapters.Presentation.Abstractions.Registrations;

public static class AdapterPresentationRegistration
{
    public static IServiceCollection RegisterAdapterPresentation(this IServiceCollection services)
    {
        services.RegisterControllers();

        return services;
    }
}
