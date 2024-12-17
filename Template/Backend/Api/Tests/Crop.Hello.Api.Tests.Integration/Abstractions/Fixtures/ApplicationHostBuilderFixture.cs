using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Crop.Hello.Api.Tests.Integration.Abstractions.Fixtures;

// IClassFixture<ApplicationHostBuilderFixture>
public class ApplicationHostBuilderFixture : IDisposable
{
    public ApplicationHostBuilderFixture()
    {
        //var inMemorySettings = new Dictionary<string, string?> {
        //    {"OpenTelemetryOptions:TeamName", "테스트"},
        //    {"OpenTelemetryOptions:ApplicationName", " "},
        //    {"OpenTelemetryOptions:Meters:0", "Microsoft.AspNetCore.Hosting"},
        //    {"OpenTelemetryOptions:Meters:1", "2"},
        //    {"OpenTelemetryOptions:Meters:2", "3"},
        //};
        var inMemorySettings = new Dictionary<string, string?> { };

        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        IHostBuilder builder = Program.CreateHostBuilder(
            args: Array.Empty<string>(),
            configuration: configuration,
            //removeJsonConfigurationSources: false);
            removeJsonConfigurationSources: true);
        Host = builder.Build();
    }

    public IHost Host { get; }

    public void Dispose() => Host.Dispose();
}
