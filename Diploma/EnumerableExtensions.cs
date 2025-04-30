namespace Diploma;

public static class EnumerableExtensions
{
    public static IQueryable<T> SkipNullable<T>(this IQueryable<T> queryable, int? skip) => skip.HasValue ? queryable.Skip(skip.Value) : queryable;

    public static IQueryable<T> TakeNullable<T>(this IQueryable<T> queryable, int? take) => take.HasValue ? queryable.Take(take.Value) : queryable;
}