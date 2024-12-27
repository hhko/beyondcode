using Crop.Hello.Api.Adapters.Infrastructure.Abstractions.Options.OpenTelemetry;
using Crop.Hello.Api.Adapters.Infrastructure.Abstractions.Options.Quartz;
using Crop.Hello.Framework.Options;
using Microsoft.Extensions.DependencyInjection;

namespace Crop.Hello.Api.Adapters.Infrastructure.Abstractions.Registration;

internal static class OptionsRegistration
{
    internal static IServiceCollection RegisterOptions(this IServiceCollection services)
    {
        services.AddOptionsWithValidation<
            OpenTelemetryOptions,
            OpenTelemetryOptionsValidator>(nameof(OpenTelemetryOptions));

        services.AddOptionsWithValidation<
            JobOptions, 
            JobOptionsValidator>(nameof(JobOptions));

        return services;
    }
}