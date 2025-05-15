using GymManagement.Adapters.Presentation.Abstractions;
using GymManagement.Domain.AggregateRoots.Sessions;
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
