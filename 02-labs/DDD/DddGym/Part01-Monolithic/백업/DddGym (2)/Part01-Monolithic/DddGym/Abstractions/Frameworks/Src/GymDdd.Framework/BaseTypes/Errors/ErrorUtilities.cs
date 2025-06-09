namespace GymDdd.Framework.BaseTypes.Errors;

public static class ErrorUtilities
{
    public static Error If(
        this Error error,
        bool condition,
        params Error[] errorsToAdd)
    {
        if (condition)
        {
            foreach (Error errorToAdd in errorsToAdd)
            {
                error = error.Combine(error);
            }
        }

        return error;
    }

    public static Fin<TValueObject> CreateValueObject<TValueObject>(
        this Error error,
        Func<TValueObject> createValueObject) where TValueObject : IValueObject
    {
        if (!error.IsEmpty)
            return error;

        return createValueObject();
    }
}
