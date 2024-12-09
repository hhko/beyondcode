using Microsoft.Extensions.Hosting;

namespace Crop.Hello.Api.Tests.Integration.Abstractions.Fixtures;

public class ApplicationHostBuilderFixture : IDisposable
{
    public ApplicationHostBuilderFixture()
    {
        var builder = Program.CreateApplicationBuilder(Array.Empty<string>());
        //builder.Services
        //    .RemoveAll<IFooService>()
        //    .AddTransient<IFooService, MockFooService>();
        // More ...

        // appsettings.Test.json
        builder.Environment.EnvironmentName = "Test";
        Host = builder.Build();
    }

    public IHost Host { get; }

    public void Dispose() => Host.Dispose();
}
