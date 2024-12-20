using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Crop.Hello.Api.Adapters.Infrastructure.Abstractions.Registration;

internal static class WindowsServiceRegistration
{
    internal static IServiceCollection RegisterWindowsService(this IServiceCollection service)
    {
        service.AddWindowsService();
        
        return service;
    }

    internal static IHostBuilder EnableWindowsService(this IHostBuilder app)
    {
        app.UseWindowsService();

        return app;
    }
}
