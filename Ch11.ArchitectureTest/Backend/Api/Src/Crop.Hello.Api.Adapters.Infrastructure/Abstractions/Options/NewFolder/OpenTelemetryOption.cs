namespace Crop.Hello.Api.Adapters.Infrastructure.Abstractions.Options.NewFolder;

public sealed class OpenTelemetryOption
{
    public string TeamName { get; set; } = string.Empty;
    public string ApplicationName { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public string OtlpCollectorHost { get; set; } = string.Empty;
    public string[] Meters { get; set; } = [];

    public bool IsLocal()
    {
        return OtlpCollectorHost.Equals("localhost");
    }
}

