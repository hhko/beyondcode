using Crop.Hello.Api.Adapters.Infrastructure.Abstractions.Options.OpenTelemetryOption;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Crop.Hello.Api.Adapters.Infrastructure.Abstractions.Registration;

internal static class OptionsRegistration
{
    internal static IServiceCollection RegisterOptions(this IServiceCollection services)
    {
        services.ConfigureOptions<OpenTelemetryOptionsSetup>();

        services.AddSingleton<IValidateOptions<OpenTelemetryOptions>, OpenTelemetryOptionsValidator>();

        return services;
    }
}
