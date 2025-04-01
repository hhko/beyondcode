---
outline: deep
---

# WebApi 통합 테스트

## 패키지
- Microsoft.AspNetCore.Mvc.Testing

## WebApi 프로젝트
```cs
public interface IAppMarker;
```

## WebApi 테스트 프로젝트
### appsettings.json
```xml
<ItemGroup>
    <Content Include="appsettings.Integration.json" CopyToOutputDirectory="PreserveNewest" />
</ItemGroup>
```

### WebApplicationFactory
```cs
// CollectionAttribute
// CollectionDefinitionAttribute    : WebAppFactoryCollectionDefinition   <- 테스트 클래스 어트리뷰트
//                                      ↓
// ICollectionFixture               : WebAppFactoryCollectionFixture
//                                      ↓
// Fixture                          : WebAppFactoryFixture                <- 주입 클래스

[CollectionDefinition(CollectionName.WebAppFactoryCollectionDefinition)]
public sealed class WebAppFactoryCollectionFixture
    : ICollectionFixture<WebAppFactoryFixture>
{
}

public sealed class WebAppFactoryFixture
    : WebApplicationFactory<IAppMarker>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        // ConfigureLogging             // 로그
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
}
```

### Controller 테스트 클래스

```cs
public abstract class ControllerTestsBase
{
    protected HttpClient _sut;

    public ControllerTestsBase(WebAppFactoryFixture webAppFactory)
    {
        // 1. CreateClient 메서드를 호출하면 IAppMarker 인스턴스를 생성합니다.
        // 2. CreateClient N번 호출해도 IAppMarker 인스턴스는 1번만 생성합니다.
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