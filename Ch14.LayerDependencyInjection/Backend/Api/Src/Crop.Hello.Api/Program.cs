using Crop.Hello.Api.Adapters.Infrastructure.Abstractions.Registration;
using Crop.Hello.Api.Adapters.Persistence.Abstractions.Registration;
using Crop.Hello.Api.Application.Abstractions.Registration;
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
            .RegisterInfrastructureLayer(builder.Environment, builder.Logging, builder.Configuration)
            .RegisterPersistenceLayer()
            .RegisterApplicationLayer();

        return builder;
    }
}