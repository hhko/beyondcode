using GymManagement.Tests.Integration.Abstractions.Fixtures;
using Xunit.Abstractions;
using static GymManagement.Tests.Unit.Abstractions.Constants.AssemblyConstants;

namespace GymManagement.Tests.Integration;

public abstract class ControllerTestsBase
{
    protected HttpClient _sut;

    public ControllerTestsBase(WebAppFactoryFixture webAppFactory)
    {
        // 1. CreateClient 메서드를 호출하면 IAppMarker 인스턴스를 생성합니다.
        // 2. CreateClient N번 호출해도 IAppMarker 인스턴스는 1번만 생성합니다.
        _sut = webAppFactory.CreateClient();
        //var _sut = webAppFactory.CreateClient(new WebApplicationFactoryClientOptions
        //{
        //    AllowAutoRedirect = false
        //});
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
        _testOutputHelper.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: 1, 생성자");
    }

    [Fact]
    public async Task Index_WhenCalled_ReturnsApplicationForm()
    {
        _testOutputHelper.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: 1");
        var response = await _sut.GetAsync("/api/weatherforecast/"); //, TestContext.Current.CancellationToken);

        response.EnsureSuccessStatusCode();

        //var responseString = await response.Content.ReadAsStringAsync();

        //Assert.Contains("Mark", responseString);
        //Assert.Contains("Evelin", responseString);

        // Arrange
        //HttpResponseMessage actual = await _sut.PostAsJsonAsync(
        //"/api/user/register",
        //    command);

        //// Assert
        //actual.IsSuccessStatusCode.Should().BeFalse();

        //string responseContent = await actual.Content.ReadAsStringAsync();
        //await VerifyJson(responseContent)
        //    .UseParameters(name);

    }
}

[Collection(CollectionName.WebAppFactoryCollectionDefinition)]
[Trait(nameof(IntegrationTest), IntegrationTest.WebApi)]
public sealed partial class WeatherForecastControllerTests2 : ControllerTestsBase
{
    private readonly ITestOutputHelper _testOutputHelper;

    public WeatherForecastControllerTests2(WebAppFactoryFixture fixture, ITestOutputHelper testOutputHelper)
        : base(fixture)
    {
        _testOutputHelper = testOutputHelper;
        _testOutputHelper.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: 2, 생성자");
    }

    //[Fact]
    //public async Task Index_WhenCalled_ReturnsApplicationForm()
    //{
    //    _testOutputHelper.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: 2");
    //    await Task.CompletedTask;
    //}
}