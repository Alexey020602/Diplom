namespace Diploma.Extensions;

public static class UpdateListExtension
{
    public static void UpdateByEnumerable<TItem>(this List<TItem> list, IEnumerable<TItem> newEnumerable)
    {
        list.RemoveAll(direction => !newEnumerable.Contains(direction));
        list.AddRange(newEnumerable.Where(direction => !list.Contains(direction)));
    }
}
