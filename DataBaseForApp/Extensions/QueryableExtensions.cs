using System.Linq.Expressions;

namespace DataBase.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<T> WhereWithNullable<T, TValue>(this IQueryable<T> queryable,
        TValue? value,
        Func<TValue, Expression<Func<T, bool>>> selector)
    {
        if (value == null) return queryable;
        
        return queryable.Where(selector(value));
    }
}