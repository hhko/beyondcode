using Microsoft.Extensions.Logging;

namespace GymManagement.Adapters.Infrastructure.Abstractions.Options.Example;

public class ExampleOptions
{
    public const string SectionName = "Example";

    //public required string LogLevel { get; init; }

    // System.InvalidOperationException:
    //  'Failed to convert configuration value at 'Example:LogLevel'
    //  to type 'Microsoft.Extensions.Logging.LogLevel'.'

    public required LogLevel LogLevel { get; init; }

    public required int Retries { get; init; }
}
