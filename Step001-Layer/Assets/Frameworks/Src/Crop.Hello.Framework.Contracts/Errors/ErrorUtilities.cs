namespace Crop.Hello.Framework.Contracts.Errors;

public static class ErrorUtilities
{
    //public static ValidationResult<TValue> ToValidationResult<TValue>(this IList<Error> errors)
    //{
    //    return ValidationResult<TValue>.WithErrors([.. errors]);
    //}

    ////Slow due to reflection usage. Prefer cache approach. See FluentValidationPipeline.CreateValidationResult method
    //public static TResult CreateValidationResult<TResult>(this ICollection<Error> errors)
    //    where TResult : class, IResult
    //{
    //    if (typeof(TResult) == typeof(Result) || typeof(TResult) == typeof(IResult))
    //    {
    //        return (ValidationResult.WithErrors(errors) as TResult)!;
    //    }

    //    object validationResult = typeof(ValidationResult<>)
    //        .GetGenericTypeDefinition()
    //        .MakeGenericType(typeof(TResult).GenericTypeArguments[0])
    //        .GetMethod(nameof(ValidationResult.WithErrors))!
    //        .Invoke(null, [errors])!;

    //    return (TResult)validationResult;
    //}

    //public static ValidationResult<TValueObject> CreateValidationResult<TValueObject>
    //(
    //    this ICollection<Error> errors,
    //    Func<TValueObject> createValueObject
    //)
    //    where TValueObject : IValueObject
    //{
    //    if (errors is null)
    //    {
    //        throw new ArgumentNullException($"{nameof(errors)} must not be null");
    //    }

    //    if (errors.Count != 0)
    //    {
    //        return ValidationResult<TValueObject>.WithErrors([.. errors]);
    //    }

    //    return ValidationResult<TValueObject>.WithoutErrors(createValueObject.Invoke());
    //}

    public static IList<Error> If
    (
        this IList<Error> errors,
        bool condition,
        params Error[] errorsToAdd
    )
    {
        if (condition is true)
        {
            foreach (Error errorToAdd in errorsToAdd)
            {
                errors.Add(errorToAdd);
            }
        }

        return errors;
    }

    //public static IList<Error> UseValidation<TValue>
    //(
    //    this IList<Error> errors,
    //    Func<IList<Error>, TValue, IList<Error>> validationSegment,
    //    TValue valueUnderValidation
    //)
    //{
    //    return validationSegment(errors, valueUnderValidation);
    //}

    //public static IList<Error> UseValidation<TValue>
    //(
    //    this IList<Error> errors,
    //    Func<TValue, IList<Error>> validationSegment,
    //    TValue valueUnderValidation
    //)
    //{
    //    var errorsToAdd = validationSegment(valueUnderValidation);

    //    foreach (var errorToAdd in errorsToAdd)
    //    {
    //        errorsToAdd.Add(errorToAdd);
    //    }

    //    return errors;
    //}
}
