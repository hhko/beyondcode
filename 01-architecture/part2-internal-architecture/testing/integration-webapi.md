---
outline: deep
---

# WebApi 통합 테스트

## 패키지
- Microsoft.AspNetCore.Mvc.Testing

## WebApi Host 프로젝트
```cs
public interface IAppMarker;
```

## Integration 테스트 프로젝트
### appsettings.json
```xml
<ItemGroup>
    <Content Include="appsettings.Integration.json" CopyToOutputDirectory="PreserveNewest" />
</ItemGroup>
```

### WebApplicationFactory
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
        // ConfigureLogging             // Logging 재정의의
        // ConfigureAppConfiguration    // IConfiguration 재정의
        // ConfigureTestServices        // IServiceCollection 재정의

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

### Controller 테스트 클래스

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