using Crop.Hello.Api.Adapters.Infrastructure.Abstractions.Options.Quartz;
using Crop.Hello.Api.Adapters.Infrastructure.Jobs;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace Crop.Hello.Api.Adapters.Infrastructure.Abstractions.Registration;

internal static class QuartzRegistration
{
    internal static IServiceCollection RegisterQuartz(this IServiceCollection services)
    {
        services.AddScoped<IJob, ProcessOutboxMessagesJob>();
        //services.AddScoped<IJob, DeleteOutdatedSoftDeletableEntitiesJob>();
        services.AddQuartz(options =>
        {
            var shedulerId = Ulid.NewUlid();
            options.SchedulerId = $"id-{shedulerId}";
            options.SchedulerName = $"name-{shedulerId}";
        });

        services.AddQuartzHostedService(options =>
        {
            options.WaitForJobsToComplete = true;
        });

        services.ConfigureOptions<QuartzOptionsSetup>();

        return services;
    }
}
