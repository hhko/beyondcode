using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Crop.Hello.Api.Adapters.Infrastructure.Abstractions.Options.OpenTelemetryOption;

internal sealed class OpenTelemetryOptionsSetup(IConfiguration configuration) : IConfigureOptions<OpenTelemetryOptions>
{
    private const string _configurationSectionName = nameof(OpenTelemetryOptions);
    private readonly IConfiguration _configuration = configuration;

    public void Configure(OpenTelemetryOptions options)
    {
        _configuration
            .GetSection(_configurationSectionName)
            .Bind(options);
    }
}
