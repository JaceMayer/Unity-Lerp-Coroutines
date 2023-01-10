public static class ArrayExtentions
{
    public static T[] Append<T>(this T[] target, T item)
    {
        var result = new T[target.Length + 1];
        target.CopyTo(result, 0);
        result[target.Length] = item;
        return result;
    }
}