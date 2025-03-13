using Microsoft.Extensions.DependencyInjection;

namespace GymManagement.Adapters.Presentation.Abstractions.Registrations;

internal static class ControllerRegistration
{
    internal static IServiceCollection RegisterControllers
    (
        this IServiceCollection services
    )
    {
        services
            .AddControllers()
            .AddApplicationPart(AssemblyReference.Assembly);
        //    .ConfigureApiBehaviorOptions(options =>
        //        options.InvalidModelStateResponseFactory = ApiBehaviorOptions.InvalidModelStateResponse);

        return services;
    }
}
