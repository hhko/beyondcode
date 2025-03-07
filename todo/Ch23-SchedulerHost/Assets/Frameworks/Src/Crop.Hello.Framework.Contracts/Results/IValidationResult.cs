using Crop.Hello.Framework.Contracts.Errors;

namespace Crop.Hello.Framework.Contracts.Results;

public interface IValidationResult
{
    Error[] ValidationErrors { get; }
}