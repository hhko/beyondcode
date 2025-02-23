using Microsoft.Extensions.DependencyInjection;

namespace DddGym.Application.Abstractions.Registrations;

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
