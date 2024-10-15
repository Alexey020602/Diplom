namespace DataBase.Extensions;

public static class FilterByStringExtension
{
    public static IQueryable<T> FilterByName<T>(this IQueryable<T> query, string? filterName, Func<T, string> nameProperty)
    {
        if (string.IsNullOrEmpty(filterName)) return query;
        
        return query.Where(t => nameProperty(t).Contains(filterName));
    }
}