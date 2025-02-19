using ErrorOr;
using FluentValidation;
using MediatR;
using System.Reflection;

namespace DddGym.Application.Abstractions.Pipelines;

// https://github.com/amantinband/error-or/issues/10
// https://github.com/amantinband/error-or?tab=readme-ov-file#mediator--fluentvalidation--erroror-

//public sealed class ValidationBehavior<TRequest, TResponse>(IValidator<TRequest>? validator = null)
//    : IPipelineBehavior<TRequest, TResponse>
//        where TRequest : IRequest<TResponse>
//        where TResponse : IErrorOr
//{
//    private readonly IValidator<TRequest>? _validator = validator;

//    public async Task<TResponse> Handle(
//        TRequest request,
//        RequestHandlerDelegate<TResponse> next,
//        CancellationToken cancellationToken)
//    {
//        if (_validator is null)
//        {
//            return await next();
//        }

//        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

//        if (validationResult.IsValid)
//        {
//            return await next();
//        }

//        var errors = validationResult.Errors
//            .ConvertAll(error => Error.Validation(
//                code: error.PropertyName,
//                description: error.ErrorMessage));

//        return (dynamic)errors;
//    }
//}

public class ValidationBehavior<TRequest, TResponse> :
    IPipelineBehavior<TRequest, TResponse>
       where TRequest : IRequest<TResponse>
       where TResponse : IErrorOr
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(
        TRequest request, 
        RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);

        var validationFailures = _validators
            .Select(v => v.Validate(context))
            .SelectMany(result => result.Errors)
            .Where(f => f != null)
            .ToList();

        if (validationFailures.Any())
        {
            var errors = validationFailures.Select(x => Error.Conflict(x.ErrorCode, x.ErrorMessage)).ToList();

            return errors.ToTResponse<TResponse>();
        }

        return await next();
    }
}

public static class ErrorOrExtensions
{
    public static TResponse ToTResponse<TResponse>(this Error error)
    {
        var errors = new List<Error> { error };

        return errors.ToTResponse<TResponse>();
    }

    public static TResponse ToTResponse<TResponse>(this List<Error> errors)
    {
        var response = (TResponse?)typeof(TResponse)
            .GetMethod(
                name: nameof(ErrorOr<object>.From),
                bindingAttr: BindingFlags.Static | BindingFlags.Public,
                types: new[] { typeof(List<Error>) })?
            .Invoke(null, new[] { errors })!;

        return response;
    }
}