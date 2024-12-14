using Crop.Hello.Api.Adapters.Infrastructure.Abstractions.Registration;
using Crop.Hello.Api.Adapters.Persistence.Abstractions.Registration;
using Crop.Hello.Api.Application.Abstractions.Registration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Hosting;

IHostBuilder builder = CreateHostBuilder(args);
using IHost host = builder.Build();
await host.RunAsync();

public static partial class Program
{
    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return CreateHostBuilder(
            args: args,
            configuration: null);
    }

    public static IHostBuilder CreateHostBuilder(
        string[] args,
        IConfiguration? configuration,
        bool removeJsonConfigurationSources = true)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                if (configuration is null)
                    return;
                 
                if (removeJsonConfigurationSources)
                {
                    //((List<IConfigurationSource>)config.Sources).RemoveAll(source => source is JsonConfigurationSource);
                    for (int i = config.Sources.Count - 1; i >= 0; i--)
                    {
                        if (config.Sources[i] is JsonConfigurationSource)
                        {
                            config.Sources.RemoveAt(i);
                        }
                    }
                }
                config.AddConfiguration(configuration);
            })
            .ConfigureServices((context, services) =>
            {
                services
                    .RegisterInfrastructureLayer(context.HostingEnvironment, context.Configuration)
                    .RegisterPersistenceLayer()
                    .RegisterApplicationLayer();
            });
    }
}