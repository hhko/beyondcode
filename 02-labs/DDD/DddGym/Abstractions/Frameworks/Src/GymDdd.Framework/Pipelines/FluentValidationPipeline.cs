using FluentValidation;
using GymDdd.Framework.Pipelines.Errors;
using GymDdd.Framework.Utilites;
using LanguageExt.Common;
using MediatR;

namespace GymDdd.Framework.Pipelines;

public sealed class FluentValidationPipeline<TRequest, TResponse>(
    IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
      where TRequest : IRequest<TResponse>
      where TResponse : class //: IResponse
{
    private readonly IEnumerable<IValidator<TRequest>> _validators = validators;

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (_validators.IsEmpty())
        {
            return await next();
        }

        Error[] errors = _validators
            .Select(validator => validator.Validate(request))
            .SelectMany(validationResult => validationResult.Errors)
            .Where(validationFailure => validationFailure is not null)
            .Select(failure => ApplicationErrors.PipelineErrors.Validator(failure.PropertyName, failure.ErrorMessage))
            .Distinct()
            .ToArray();

        if (errors.Length is not 0)
        {
            return (dynamic)Error.Many(errors);
        }

        return await next();
    }
}
