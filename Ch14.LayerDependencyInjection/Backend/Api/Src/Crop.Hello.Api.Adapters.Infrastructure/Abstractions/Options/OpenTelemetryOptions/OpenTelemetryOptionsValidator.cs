using Crop.Hello.Framework.Utilities;
using Microsoft.Extensions.Options;

namespace Crop.Hello.Api.Adapters.Infrastructure.Abstractions.Options.OpenTelemetryOptions;

internal sealed class OpenTelemetryOptionsValidator : IValidateOptions<OpenTelemetryOptions>
{
    public ValidateOptionsResult Validate(string? name, OpenTelemetryOptions options)
    {
        string validationResult = string.Empty;

        if (options.TeamName.IsNullOrEmptyOrWhiteSpace())
        {
            validationResult += "TeamName is missing. ";
        }

        if (options.ApplicationName.IsNullOrEmptyOrWhiteSpace())
        {
            validationResult += "Host is missing. ";
        }

        if (options.Version.IsNullOrEmptyOrWhiteSpace())
        {
            validationResult += "Version is missing. ";
        }

        if (options.OtlpCollectorHost.IsNullOrEmpty())
        {
            validationResult += "OtlpCollectorHost is missing. ";
        }

        if (options.Meters.IsNullOrEmpty())
        {
            validationResult += "Meters are null or empty. ";
        }

        if (!validationResult.IsNullOrEmptyOrWhiteSpace())
        {
            return ValidateOptionsResult.Fail(validationResult);
        }

        return ValidateOptionsResult.Success;
    }
}
