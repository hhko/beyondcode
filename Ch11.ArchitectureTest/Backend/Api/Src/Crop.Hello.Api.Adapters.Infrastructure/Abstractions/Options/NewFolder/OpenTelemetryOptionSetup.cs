using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Crop.Hello.Api.Adapters.Infrastructure.Abstractions.Options.NewFolder;

internal sealed class OpenTelemetryOptionSetup(IConfiguration configuration) : IConfigureOptions<OpenTelemetryOption>
{
    private const string _configurationSectionName = nameof(OpenTelemetryOption);
    private readonly IConfiguration _configuration = configuration;

    public void Configure(OpenTelemetryOption options)
    {
        _configuration
            .GetSection(_configurationSectionName)
            .Bind(options);
    }
}
