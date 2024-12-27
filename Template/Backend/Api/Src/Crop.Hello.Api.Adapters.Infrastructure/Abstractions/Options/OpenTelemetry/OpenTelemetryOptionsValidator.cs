using FluentValidation;

namespace Crop.Hello.Api.Adapters.Infrastructure.Abstractions.Options.OpenTelemetry;

internal sealed class OpenTelemetryOptionsValidator
    : AbstractValidator<OpenTelemetryOptions>
{
    public OpenTelemetryOptionsValidator()
    {
        RuleFor(x => x.TeamName).NotEmpty();
        RuleFor(x => x.ApplicationName).NotEmpty();
        RuleFor(x => x.Version).NotEmpty();
        RuleFor(x => x.OtlpCollectorHost).NotEmpty();

        // 배열
        RuleForEach(x => x.Meters).NotEmpty();
    }
}