using DataBase.Models;

namespace Diploma.Extensions;
//public static class QuirableFilterExtension
//{
//    public static IQueryable<T> FilterBy<T, K>(this IQueryable<T> source, K? key,  Func<T, K> path) where K: struct, IEquatable<K>
//    {
//        if (!key.HasValue) return source;

//        return source.Where(element => path(element).Equals(key.Value));
//    }

//    public static IQueryable<T> FilterBy<T, K>(this IQueryable<T> source, K? key, Func<T, K> path) where K: class, IEquatable<K>
//    {
//        if (key is null) return source;

//        return source.Where(element => path(element).Equals(key));
//    }
//}
public static class DivisionQuiryableFilterExtension
{
    public static IQueryable<Division> FilterByFaculty(this IQueryable<Division> query, int? facultyId)
    {
        if (!facultyId.HasValue) return query;

        return query.Where(division => division.Faculty.Id == facultyId.Value);
    }
}
