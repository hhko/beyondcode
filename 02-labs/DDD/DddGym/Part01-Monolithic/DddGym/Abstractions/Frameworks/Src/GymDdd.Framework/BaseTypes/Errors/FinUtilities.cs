namespace GymDdd.Framework.BaseTypes.Errors;

public static class FinUtilities
{
    public static Fin<T> CombineErrors<T>(this Fin<T> fin, Error newError)
    {
        return fin.MapFail(error => error.Combine(newError));
    }
}
