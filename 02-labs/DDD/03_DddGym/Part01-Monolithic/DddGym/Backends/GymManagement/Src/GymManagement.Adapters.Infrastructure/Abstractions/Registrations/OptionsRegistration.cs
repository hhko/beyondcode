using GymDdd.Framework.Options;
using GymManagement.Adapters.Infrastructure.Abstractions.Options;
using GymManagement.Adapters.Infrastructure.Authentication.TokenGenerator;
using Microsoft.Extensions.DependencyInjection;

namespace GymManagement.Adapters.Infrastructure.Abstractions.Registrations;

internal static class OptionsRegistration
{
    public static IServiceCollection RegisterOptions(this IServiceCollection services)
    {
        services.AddConfigureOptions<ExampleOptions, ExampleOptions.Validator>(ExampleOptions.SectionName);
        services.AddConfigureOptions<JwtOptions, JwtOptions.Validator>(JwtOptions.SectionName);

        return services;
    }
}
