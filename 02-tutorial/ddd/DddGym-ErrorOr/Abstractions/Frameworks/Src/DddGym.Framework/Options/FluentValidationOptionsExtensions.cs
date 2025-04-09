using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DddGym.Framework.Options;

public static class FluentValidationOptionsExtensions
{
    public static OptionsBuilder<TOptions> AddConfigureOptions<TOptions, TValidator>(
        this IServiceCollection services,
        string configurationSectionName)
            where TOptions : class
            where TValidator : class, IValidator<TOptions>
    {
        // TOptions의 IValidator 등록
        services.AddScoped<IValidator<TOptions>, TValidator>();

        // TOptions의 IValidator 검사
        return services.AddOptions<TOptions>()
            .BindConfiguration(configurationSectionName)    // appsettings.json의 Section 이름
            .ValidateFluentValidation()                     // OptionsBuilder<TOptions>
            .ValidateOnStart();
    }
}