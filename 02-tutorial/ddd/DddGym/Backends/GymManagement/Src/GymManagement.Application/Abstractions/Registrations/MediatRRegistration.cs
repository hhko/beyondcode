using FunctionalDdd.Framework.Pipelines;
using Microsoft.Extensions.DependencyInjection;

namespace GymManagement.Application.Abstractions.Registrations;

internal static class MediatRRegistration
{
    internal static IServiceCollection RegisterMediatR(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssemblies(AssemblyReference.Assembly);

            //// IPipelineBehavior 호출 순서는 중요하다: 데코레이터 순서
            ////cfg.AddOpenBehavior(typeof(LoggingPipeline<,>));

            configuration.AddOpenBehavior(typeof(FluentValidationPipeline<,>));
            //cfg.AddOpenBehavior(typeof(LoggingPipeline<,>));
            ////cfg.AddOpenBehavior(typeof(QueryCachingPipeline<,>));
            ////cfg.AddBehavior<CreateOrderHeaderOpenTelemetryPipeline>();

            ////configuration.AddOpenBehavior(typeof(FluentValidationPipeline<,>));
            ////configuration.AddOpenBehavior(typeof(LoggingPipeline<,>));
            ////configuration.AddOpenBehavior(typeof(QueryCachingPipeline<,>));
            ////configuration.AddBehavior<CreateOrderHeaderOpenTelemetryPipeline>();
        });

        return services;
    }
}
