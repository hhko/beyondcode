namespace Crop.Hello.Api.Adapters.Infrastructure.Abstractions.Options.OpenTelemetry;

public sealed class OpenTelemetryOptions
{
    public string TeamName { get; init; } = default!;
    public string ApplicationName { get; init; } = default!;
    public string Version { get; init; } = default!;
    public string OtlpCollectorHost { get; init; } = default!;
    public string[] Meters { get; init; } = [];

    public bool IsLocal()
    {
        return OtlpCollectorHost.Equals("localhost");
    }
}

