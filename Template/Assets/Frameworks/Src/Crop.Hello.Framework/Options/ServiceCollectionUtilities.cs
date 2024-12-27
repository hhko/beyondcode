using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Crop.Hello.Framework.Options;

public static class ServiceCollectionUtilities
{
    public static OptionsBuilder<TOptions> AddOptionsWithValidation<TOptions, TValidator>(
        this IServiceCollection services,
        string configurationSection)
        where TOptions : class
        where TValidator : class, IValidator<TOptions>
    {
        // Add the validator
        services.AddScoped<IValidator<TOptions>, TValidator>();

        return services.AddOptions<TOptions>()
            .BindConfiguration(configurationSection)    // IConfigureOptions<OpenTelemetryOptions>
            .ValidateFluentValidation()                 // 유효성 검사
            .ValidateOnStart();
    }
}