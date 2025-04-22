using GymManagement.Tests.Integration.Abstractions;
using GymManagement.Tests.Integration.Abstractions.Fixtures;
using Xunit.Abstractions;
using static GymManagement.Tests.Unit.Abstractions.Constants.AssemblyConstants;

namespace GymManagement.Tests.Integration;

[Collection(CollectionName.WebAppFactoryCollectionDefinition)]
[Trait(nameof(IntegrationTest), IntegrationTest.WebApi)]
public sealed partial class WeatherForecastControllerTests : ControllerTestsBase
{
    private readonly ITestOutputHelper _testOutputHelper;

    public WeatherForecastControllerTests(WebAppFactoryFixture fixture, ITestOutputHelper testOutputHelper)
        : base(fixture)
    {
        //_testOutputHelper = testOutputHelper;
        //_testOutputHelper.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: 1, 생성자");
    }

    [Fact]
    public async Task Index_WhenCalled_ReturnsApplicationForm()
    {
        _testOutputHelper.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: 1");

        using var client = _webAppFactory.CreateClient();
        var response = await client.GetAsync("/api/weatherforecast/"); //, TestContext.Current.CancellationToken);

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