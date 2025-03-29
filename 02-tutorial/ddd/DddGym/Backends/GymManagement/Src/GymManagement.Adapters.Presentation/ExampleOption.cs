using FluentValidation;
using Microsoft.Extensions.Logging;

namespace GymManagement.Adapters.Presentation;

public class ExampleOptions
{
    public const string SectionName = "Example";

    //public required string LogLevel { get; init; }

    // System.InvalidOperationException:
    //  'Failed to convert configuration value at 'Example:LogLevel'
    //  to type 'Microsoft.Extensions.Logging.LogLevel'.'

    public required LogLevel LogLevel { get; init; }

    public required int Retries {  get; init; }
}

internal sealed class ExampleOptionsValidator : AbstractValidator<ExampleOptions>
{
    public ExampleOptionsValidator()
    {
        RuleFor(x => x.Retries)
            .InclusiveBetween(1, 9);
    }
}