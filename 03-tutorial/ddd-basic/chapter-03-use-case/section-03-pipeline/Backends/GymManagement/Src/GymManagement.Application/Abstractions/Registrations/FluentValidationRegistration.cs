using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace GymManagement.Application.Abstractions.Registrations;

internal static class FluentValidationRegistration
{
    internal static IServiceCollection RegisterFluentValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(
            assembly: AssemblyReference.Assembly,
            includeInternalTypes: true);

        return services;
    }
}
