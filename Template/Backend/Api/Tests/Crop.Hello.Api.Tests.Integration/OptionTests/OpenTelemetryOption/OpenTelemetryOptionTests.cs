using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using static Crop.Hello.Api.Tests.Integration.Abstractions.Constants.Constants;

namespace Crop.Hello.Api.Tests.Integration.OptionTests.OpenTelemetryOption;

[Trait(nameof(IntegrationTest), IntegrationTest.Option)]
public class OpenTelemetryOptionTests
{
    //{
    //  "OpenTelemetryOptions": {
    //    "TeamName": "better-code-with-ddd",
    //    "ApplicationName": "Crop.Hello.Api",
    //    "Version": "1.0.100",
    //    "OtlpCollectorHost": "localhost",
    //    "Meters": [
    //      "Microsoft.AspNetCore.Hosting",
    //      "Microsoft.AspNetCore.Server.Kestrel",
    //      "System.Net.Http"
    //    ]
    //  }
    //}

    //public static IEnumerable<object[]> TestDictionaries =>
    //    new List<object[]>
    //    {
    //        //// Empty
    //        //new object[] { new Dictionary<string, string?> {
    //        //} },

    //        // ...
    //        new object[] { new Dictionary<string, string?> {
    //            {"OpenTelemetryOptions:TeamName", " "},
    //            //{"OpenTelemetryOptions:ApplicationName", ""},
    //            //{"OpenTelemetryOptions:Version", ""},
    //            //{"OpenTelemetryOptions:OtlpCollectorHost", ""},
    //            //{"OpenTelemetryOptions:Meters:0", "Microsoft.AspNetCore.Hosting"},
    //            //{"OpenTelemetryOptions:Meters:1", "2"},
    //            //{"OpenTelemetryOptions:Meters:2", "3"},
    //        } },

    //        // Invalid: OtlpCollectorHost
    //        new object[] { new Dictionary<string, string?> {
    //            //{"OpenTelemetryOptions:TeamName", "TeamName"},
    //            //{"OpenTelemetryOptions:ApplicationName", "ApplicationName"},
    //            //{"OpenTelemetryOptions:Version", "Version"},
    //            {"OpenTelemetryOptions:OtlpCollectorHost", " "},
    //            //{"OpenTelemetryOptions:Meters:0", "Microsoft.AspNetCore.Hosting"},
    //            //{"OpenTelemetryOptions:Meters:1", "2"},
    //            //{"OpenTelemetryOptions:Meters:2", "3"},
    //        } }
    //    };

    //[Theory]
    //[MemberData(nameof(TestDictionaries))]
    //public void OpenTelemetryOptionsValidator_ShouldThrow_FromInMemory(Dictionary<string, string?> inMemorySettings)
    //{
    //    // Arragne
    //    IConfiguration configuration = new ConfigurationBuilder()
    //        .AddInMemoryCollection(inMemorySettings)
    //        .Build();

    //    // Act
    //    IHostBuilder builder = Program.CreateHostBuilder(
    //        args: Array.Empty<string>(),
    //        configuration: configuration,
    //        removeJsonConfigurationSources: false);
    //    Action act = () => builder.Build();

    //    // Assert
    //    act.Should().Throw<OptionsValidationException>();
    //}

    [Theory]
    [InlineData("./OptionTests/OpenTelemetryOption/appsettings.Invalid.TeamName.json")]
    [InlineData("./OptionTests/OpenTelemetryOption/appsettings.Invalid.ApplicationName.json")]
    public void OpenTelemetryOptionsValidator_ShouldThrow_FromJsonFile(string jsonFilePath)
    {
        // Arragne
        IConfiguration configuration = new ConfigurationBuilder()
            //.AddEnvironmentVariables()
            .AddJsonFile(jsonFilePath)
            .Build();

        // Act
        IHostBuilder builder = Program.CreateHostBuilder(
            args: Array.Empty<string>(),
            configuration: configuration,
            removeJsonConfigurationSources: true);
        Action act = () => builder.Build();

        // Assert
        act.Should().Throw<OptionsValidationException>();
    }
}
