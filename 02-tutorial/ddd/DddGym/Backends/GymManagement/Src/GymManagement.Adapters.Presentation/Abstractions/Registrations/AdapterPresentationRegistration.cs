using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace GymManagement.Adapters.Presentation.Abstractions.Registrations;

public static class AdapterPresentationRegistration
{
    public static IServiceCollection RegisterAdapterPresentation(this IServiceCollection services)
    {
        services.RegisterControllers();

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

        return services;
    }
}
