using DddGym.Framework.Options;
using DddGym.Framework.Utilites;
using FluentValidation;
using GymManagement.Adapters.Infrastructure.Abstractions.Options.Example;
using GymManagement.Domain.Abstractions.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace GymManagement.Adapters.Infrastructure.Abstractions.Registrations;

internal static class OptionsRegistration
{
    public static IServiceCollection RegisterOptions(this IServiceCollection services)
    {
        // IValidator 등록
        services.AddValidatorsFromAssembly(
                assembly: AssemblyReference.Assembly,
                includeInternalTypes: true,

                // ServiceLifetime.Scoped 기본 값일 때 예외가 발생합니다.
                //
                // System.InvalidOperationException:
                //  'Cannot resolve scoped service
                //      'FluentValidation.IValidator`1[GymManagement.Adapters.Presentation.ExampleOptions]'
                //      from root provider.'
                lifetime: ServiceLifetime.Singleton);

        // 옵션 등록
        services.ConfigureOptions<ExampleOptions> (ExampleOptions.SectionName);

        return services;
    }

    private static IServiceCollection ConfigureOptions<T>(this IServiceCollection services, string sectionName) where T : class
    {
        if (sectionName.IsNullOrEmptyOrWhiteSpace() is true)
        {
            throw new ArgumentException($"Section name for {typeof(T).Name} cannot be null or empty.", nameof(sectionName));
        }

        services
            .AddOptions<T>()
            .BindConfiguration(sectionName)
            .ValidateFluently()
            .ValidateOnStart();

        return services;
    }
}


    