namespace Crop.Hello.Framework.Utilities;

public static class ListUtilities
{
    public static bool NotNullOrEmpty<TValue>(this IList<TValue> list)
    {
        return list is not null && list.Count > 0;
    }

    public static bool IsNullOrEmpty<TValue>(this IList<TValue> list)
    {
        return list.NotNullOrEmpty() is false;
    }
}
