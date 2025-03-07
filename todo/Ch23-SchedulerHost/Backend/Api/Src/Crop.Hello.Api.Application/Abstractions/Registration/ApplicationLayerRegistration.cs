using Microsoft.Extensions.DependencyInjection;

namespace Crop.Hello.Api.Application.Abstractions.Registration;

public static class ApplicationLayerRegistration
{
    public static IServiceCollection RegisterApplicationLayer(
        this IServiceCollection services)
    {
        return services;
    }
}