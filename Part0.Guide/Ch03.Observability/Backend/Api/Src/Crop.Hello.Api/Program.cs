using Crop.Hello.Api;
using Crop.Hello.Api.Adapters.Infrastructure.Abstractions.Registration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

HostApplicationBuilder builder = CreateApplicationBuilder(args);
using IHost host = builder.Build();
await host.RunAsync();

public static partial class Program
{
    public static HostApplicationBuilder CreateApplicationBuilder(string[] args)
    {
        HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

        builder.Services
            .RegisterPersistenceLayer(builder.Environment, builder.Logging, builder.Configuration);

        builder.Services.AddTransient<Class1>();

        return builder;
    }
}