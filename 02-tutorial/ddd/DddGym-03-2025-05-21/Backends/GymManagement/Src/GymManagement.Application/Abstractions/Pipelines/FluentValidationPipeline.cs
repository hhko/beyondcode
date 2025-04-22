namespace GymManagement.Application.Abstractions.Pipelines;

// TODO: LanguageExt IErrorOr
//// https://github.com/amantinband/error-or/issues/10

//// TODO: FluentValidationPipeline 전용 테스트 작성

//// https://github.com/amantinband/error-or?tab=readme-ov-file#mediator--fluentvalidation--erroror-
//public class FluentValidationPipeline<TRequest, TResponse>(IValidator<TRequest>? validator = null)
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

//        // TODO: 타입 변환
//        // 에러
//        //  'Cannot implicitly convert type 'System.Collections.Generic.List <ErrorOr.Error>'
//        //      to 'ErrorOr.IErrorOr<GymManagement.Application.Usecases.Subscriptions.Commands.CreateSubscription.CreateSubscriptionResponse>'.
//        //   An explicit conversion exists (are you missing a cast?)'
//        return (dynamic)errors;
//        //return ErrorOr<TResponse>.From(errors);
//    }
//}

//public sealed class FluentValidationPipeline<TRequest, TResponse>(
//    IEnumerable<IValidator<TRequest>> validators)
//    : IPipelineBehavior<TRequest, TResponse>
//        where TRequest : IRequest<TResponse>
//        where TResponse : class, IErrorOr
//{
//    private readonly IEnumerable<IValidator<TRequest>> _validators = validators;

//    public async Task<TResponse> Handle(
//        TRequest request,
//        RequestHandlerDelegate<TResponse> next,
//        CancellationToken cancellationToken)
//    {
//        // IValidator가 없을 때
//        if (_validators.IsEmpty())
//        {
//            return await next();
//        }

//        Error[] errors = _validators
//            .Select(validator => validator.Validate(request))
//            .SelectMany(validationResult => validationResult.Errors)
//            .Where(validationFailure => validationFailure is not null)
//            .Select(failure =>
//                Error.Validation(
//                    code: failure.PropertyName,
//                    description: failure.ErrorMessage))
//            .Distinct()
//            .ToArray();

//        if (errors.Length is not 0)
//        {
//            // Microsoft.CSharp.RuntimeBinder.RuntimeBinderException:
//            //  Cannot implicitly convert type
//            //      'ErrorOr.Error[]' to
//            //      'ErrorOr.IErrorOr <GymManagement.Application.Usecases.Subscriptions.Queries.ListSubscriptions.SubscriptionsResponse> ''

//            //return (TResponse)ValidationResultCache[typeof(TResponse)](errors);
//            //return (dynamic)errors;

//            var response = (TResponse?)typeof(TResponse)
//                .GetMethod(
//                    name: nameof(ErrorOr<object>.From),
//                    bindingAttr: BindingFlags.Static | BindingFlags.Public,
//                    types: [typeof(List<Error>)])?
//                .Invoke(null, [errors])!;

//            return response;
//        }

//        return await next();
//    }
//}

//public class ModelValidationBehaviour<TRequest, IResult>
//    (IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, IResult>
//    where TRequest : IRequest<IResult>
//{
//    private readonly IEnumerable<IValidator<TRequest>> _validators = validators;

//    public async Task<IResult> Handle(TRequest request,
//        RequestHandlerDelegate<IResult> next, CancellationToken cancellationToken)
//    {
//        if (!_validators.Any())
//        {
//            return await next();
//        }

//        var context = new ValidationContext<TRequest>(request);

//        var validationResults = _validators.Select(v => v.Validate(context)).ToList();
//        var groupedValidationFailures = validationResults.SelectMany(v => v.Errors)
//            .GroupBy(e => e.PropertyName)
//            .Select(g => new {
//                PropertyName = g.Key,
//                ValidationFailures = g.Select(v => new { v.ErrorMessage })
//            }).ToList();

//        if (groupedValidationFailures.Count != 0)
//        {
//            var validationProblemsDictionary = new Dictionary<string, string[]>();
//            foreach (var group in groupedValidationFailures)
//            {
//                var errorMessages = group.ValidationFailures.Select(v => v.ErrorMessage);
//                validationProblemsDictionary.Add(group.PropertyName, errorMessages.ToArray());
//            }

//            return (IResult)Results.ValidationProblem(validationProblemsDictionary);
//        }

//        return await next();
//    }
//}

//public class FluentValidationPipeline<TRequest, TResponse>(IValidator<TRequest>? validator = null)
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

//public class ValidationBehavior<TRequest, TResponse> :
//    IPipelineBehavior<TRequest, TResponse>
//       where TRequest : IRequest<TResponse>
//       where TResponse : IErrorOr
//{
//    private readonly IEnumerable<IValidator<TRequest>> _validators;

//    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
//    {
//        _validators = validators;
//    }

//    public async Task<TResponse> Handle(
//        TRequest request,
//        RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
//    {
//        var context = new ValidationContext<TRequest>(request);

//        var validationFailures = _validators
//            .Select(v => v.Validate(context))
//            .SelectMany(result => result.Errors)
//            .Where(f => f != null)
//            .ToList();

//        if (validationFailures.Any())
//        {
//            var errors = validationFailures.Select(x => Error.Conflict(x.ErrorCode, x.ErrorMessage)).ToList();

//            return errors.ToTResponse<TResponse>();
//        }

//        return await next();
//    }
//}

//public static class ErrorOrExtensions
//{
//    public static TResponse ToTResponse<TResponse>(this Error error)
//    {
//        var errors = new List<Error> { error };

//        return errors.ToTResponse<TResponse>();
//    }

//    public static TResponse ToTResponse<TResponse>(this List<Error> errors)
//    {
//        var response = (TResponse?)typeof(TResponse)
//            .GetMethod(
//                name: nameof(ErrorOr<object>.From),
//                bindingAttr: BindingFlags.Static | BindingFlags.Public,
//                types: new[] { typeof(List<Error>) })?
//            .Invoke(null, new[] { errors })!;

//        return response;
//    }
//}