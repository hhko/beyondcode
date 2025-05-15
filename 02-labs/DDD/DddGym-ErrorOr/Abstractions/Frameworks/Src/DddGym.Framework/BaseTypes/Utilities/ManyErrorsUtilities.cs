using LanguageExt;
using LanguageExt.Common;

namespace DddGym.Framework.BaseTypes.Utilities;

public static class ManyErrorsUtilities
{
    public static ManyErrors If(
        this ManyErrors error,
        bool condition,
        params Error[] errorsToAdd)
    {
        if (condition)
        {
            foreach (Error errorToAdd in errorsToAdd)
            {
                return new ManyErrors([error, errorToAdd]);
            }
        }

        return error;
    }

    public static Fin<TValueObject> CreateValidation<TValueObject>(
        this ManyErrors errors,
        Func<TValueObject> createValueObject)
        where TValueObject : IValueObject
    {
        if (errors is null)
        {
            throw new ArgumentNullException($"{nameof(errors)} must not be null");
        }

        if (errors.Count != 0)
        {
            return errors;
        }

        return createValueObject();
    }
}
