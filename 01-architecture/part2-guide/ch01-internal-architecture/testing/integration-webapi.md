- Microsoft.AspNetCore.Mvc.Testing

```cs
public interface IAppMarker;
```

```xml
<ItemGroup>
    <Content Include="appsettings.Integration.json" CopyToOutputDirectory="PreserveNewest" />
</ItemGroup>
```

```cs
// CollectionAttribute
// CollectionDefinitionAttribute    : WebAppFactoryCollectionDefinition
//                                      ↓
// ICollectionFixture               : WebAppFactoryCollectionFixture
//                                      ↓
// Fixture                          : WebAppFactoryFixture

[CollectionDefinition(CollectionName.WebAppFactoryCollectionDefinition)]
public sealed class WebAppFactoryCollectionFixture
    : ICollectionFixture<WebAppFactoryFixture>
{
}

public sealed class WebAppFactoryFixture
    : WebApplicationFactory<IAppMarker>
    , IAsyncLifetime
{
    public async ValueTask InitializeAsync()
    {
        await Task.CompletedTask;
    }

    public async override ValueTask DisposeAsync()
    {
        await base.DisposeAsync();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        // ConfigureLogging             // 로그
        // ConfigureAppConfiguration    // IConfiguration
        // ConfigureTestServices        // IServiceCollection

        builder.ConfigureAppConfiguration(context =>
        {
            // appsettings.Test.json
            //  - Content
            //  - PreserveNewest
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
}
```

```cs
public abstract class ControllerTestsBase
{
    protected HttpClient _sut;

    public ControllerTestsBase(WebAppFactoryFixture webAppFactory)
    {
        // 1. CreateClient 메서드를 호출하면 Program 인스턴스를 생성합니다.
        // 2. CreateClient N번 호출해도 Program 인스턴스는 1번만 생성합니다.
        _sut = webAppFactory.CreateClient();
    }
}

[Collection(CollectionName.WebAppFactoryCollectionDefinition)]
[Trait(nameof(IntegrationTest), IntegrationTest.WebApi)]
public sealed partial class WeatherForecastControllerTests : ControllerTestsBase
{
    private readonly ITestOutputHelper _testOutputHelper;

    public WeatherForecastControllerTests(WebAppFactoryFixture fixture, ITestOutputHelper testOutputHelper)
        : base(fixture)
    {
        _testOutputHelper = testOutputHelper;
    }
```