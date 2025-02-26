using GymManagement.Application.Abstractions.Pipelines;
using Microsoft.Extensions.DependencyInjection;

namespace GymManagement.Application.Abstractions.Registrations;

internal static class MediatRRegistration
{
    internal static IServiceCollection RegisterMediatR(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(AssemblyReference.Assembly);

            // IPipelineBehavior 호출 순서는 중요하다: 데코레이터 순서
            //cfg.AddOpenBehavior(typeof(LoggingPipeline<,>));
            cfg.AddOpenBehavior(typeof(FluentValidationPipeline<,>));
        });

        return services;
    }
}
