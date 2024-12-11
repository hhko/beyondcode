using Crop.Hello.Api.Tests.Integration.Abstractions.Fixtures;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using static Crop.Hello.Api.Tests.Integration.Abstractions.Constants.Constants;

namespace Crop.Hello.Api.Tests.Integration;

[Trait(nameof(IntegrationTest), IntegrationTest.Host)]
public class HostTests : IClassFixture<ApplicationHostBuilderFixture>
{
    private readonly IHost _host;

    public HostTests(ApplicationHostBuilderFixture fixture)
    {
        _host = fixture.Host;
    }

    [Fact]
    public void Logger()
    {
        //Class1 c1 = _host.Services.GetRequiredService<Class1>();
    }
}
