using Microsoft.Extensions.Logging;

namespace GymManagement.Adapters.Infrastructure.Abstractions.Options.Example;

public sealed class ExampleOptions
{
    public const string SectionName = "Example";

    public required LogLevel LogLevel { get; init; }

    public required int Retries { get; init; }
}
