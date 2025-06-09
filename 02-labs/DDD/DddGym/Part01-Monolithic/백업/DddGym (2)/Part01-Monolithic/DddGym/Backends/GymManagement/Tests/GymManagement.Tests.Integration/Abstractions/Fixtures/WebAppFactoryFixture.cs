using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using static GymManagement.Tests.Unit.Abstractions.Constants.AssemblyConstants;

namespace GymManagement.Tests.Integration.Abstractions.Fixtures;

// CollectionAttribute                                                    <- 컬랙션 어튜리뷰트 사용
// CollectionDefinitionAttribute    : WebAppFactoryCollectionDefinition   <- Collection 어트리뷰트 정의
//                                      ↓
// ICollectionFixture               : WebAppFactoryCollectionFixture      <- Collection Fixture
//                                      ↓
// {Fixture}                        : WebAppFactoryFixture                <- Fixture

[CollectionDefinition(CollectionName.WebAppFactoryCollectionDefinition)]
public sealed class WebAppFactoryCollectionFixture
    : ICollectionFixture<WebAppFactoryFixture>
{
}

public sealed class WebAppFactoryFixture
    : WebApplicationFactory<IAppMarker>
    , IAsyncLifetime
{
    public async Task InitializeAsync()
    {
        await Task.CompletedTask;
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        await Task.CompletedTask;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        // ConfigureLogging             // ILoggingBuilder
        // ConfigureAppConfiguration    // IConfiguration
        // ConfigureTestServices        // IServiceCollection

        builder.ConfigureAppConfiguration(context =>
        {
            // appsettings.json과 appsettings.Development.json 제거
            RemoveJsonConfigurationSources(context);

            // appsettings.Integration.json 추가
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile(IntegrationTest.Appsettings_Integration_Json)
                .AddEnvironmentVariables()
                .Build();

            context.AddConfiguration(configuration);
        });

        builder.ConfigureTestServices(services =>
        {
            // 의존성
        });

        // Environment
        //builder.UseEnvironment("Development");
    }

    private static void RemoveJsonConfigurationSources(IConfigurationBuilder context)
    {
        var filteredSources = context.Sources
            .Where(source => source is not JsonConfigurationSource)
            .ToList();

        context.Sources.Clear();
        foreach (var source in filteredSources)
        {
            context.Sources.Add(source);
        }
    }

    //public WebApplicationFactory<IAppMarker> WithMockService(Action<IServiceCollection> overrideServices)
    //{
    //    return WithWebHostBuilder(builder =>
    //    {
    //        builder.ConfigureTestServices(overrideServices);
    //    });
    //}
}
