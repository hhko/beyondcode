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
        services.AddConfigureOptions<ExampleOptions, ExampleOptionsValidator>(ExampleOptions.SectionName);

        return services;
    }
}


    