namespace Common.Extensions;

public static class ListExtensions
{
    public static List<List<T>> Split<T>(this List<T> list, int chunkSize)
    {
        var splitLists = new List<List<T>>();

        for (int i = 0; i < list.Count; i += chunkSize)
        {
            splitLists.Add(list.GetRange(i, Math.Min(chunkSize, list.Count - i)));
        }

        return splitLists;
    }
}
