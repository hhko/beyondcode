using GymManagement.Application.Abstractions.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace GymManagement.Application.Abstractions.Registrations;

public static class ApplicationRegistration
{
    public static IServiceCollection RegisterApplication(this IServiceCollection services)
    {
        services
            .RegisterFluentValidation()
            .RegisterMediatR();

        return services;
    }
}