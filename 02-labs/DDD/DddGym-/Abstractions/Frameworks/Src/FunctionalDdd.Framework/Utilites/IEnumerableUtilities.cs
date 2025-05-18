using System.Collections;

namespace FunctionalDdd.Framework.Utilites;

public static class IEnumerableUtilities
{
    public static string Join<TValue>(this IEnumerable<TValue> items, char separator)
    {
        return string.Join(separator, items);
    }

    public static string Join<TValue>(this IEnumerable<TValue> items, string separator)
    {
        return string.Join(separator, items);
    }

    public static bool Any(this IEnumerable source)
    {
        foreach (var _ in source)
        {
            return true;
        }

        return false;
    }

    public static bool IsEmpty(this IEnumerable source)
    {
        return source.Any() is false;
    }
}
