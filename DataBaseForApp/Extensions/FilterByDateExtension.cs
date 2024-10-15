using System.Linq.Expressions;

namespace DataBase.Extensions;

public static class FilterByDateExtension
{
    public static IQueryable<T> FilterByDate<T>(
        this IQueryable<T> queryable, 
        DateTime? date,
        Func<T, DateTime?> getDateTime)
    {
        if (date is null) return queryable;
        
        return queryable.Where(d => getDateTime(d) == date.Value);
    }

    public static IQueryable<T> FilterByDate<T>(
        this IQueryable<T> queryable,
        DateOnly? date,
        Func<T, DateTime> getDateTime)
    {
        if (date is null) return queryable;

        return queryable.Where(FilterByDateOnly(date.Value, getDateTime));
    }

    private static Expression<Func<T, bool>> FilterByDateOnly<T>(
        DateOnly date,
        Func<T, DateTime> getDateTime)
    {
        return t => DateOnly.FromDateTime(getDateTime(t)) == date;
    }
}