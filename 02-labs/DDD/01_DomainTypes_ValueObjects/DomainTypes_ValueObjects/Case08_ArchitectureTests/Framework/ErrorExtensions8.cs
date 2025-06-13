namespace DomainTypes_ValueObjects.Case08_ArchitectureTests.Framework;

public static class ErrorExtensions8
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
                error = error.Combine(errorToAdd);
            }
        }

        return error;
    }

    public static Fin<TValueObject> CreateValueObject<TValueObject>(
        this Error error,
        Func<TValueObject> createValueObject) where TValueObject : IValueObject8
    {
        if (!error.IsEmpty)
            return error;

        return createValueObject();
    }
}