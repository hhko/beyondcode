using GymManagement.Adapters.Presentation.Abstractions;
using GymManagement.Application.Usecases.Sessions.Ports;
using Microsoft.Extensions.DependencyInjection;

namespace GymManagement.Adapters.Infrastructure.Abstractions.Registrations;

internal static class SystemRegistration
{
    internal static IServiceCollection RegisterSystem(this IServiceCollection services)
    {
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        return services;
    }
}
