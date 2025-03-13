using System.Collections;

namespace GymManagement.Domain.Abstractions.Utilities;

public static class IEnumerableUtilities
{
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