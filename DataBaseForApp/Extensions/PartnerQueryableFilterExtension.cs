using DataBase.Models;

namespace DataBase.Extensions;

public static class PartnerQueryableFilterExtension
{
    public static IQueryable<Partner> FilterByType(this IQueryable<Partner> query, int? partnerTypeId)
    {
        if (!partnerTypeId.HasValue) return query;

        return query.Where(partner => partner.PartnerType.Id == partnerTypeId.Value);
    }
    public static IQueryable<Partner> FilterByDirection(this IQueryable<Partner> query, int? directionId)
    {
        if (!directionId.HasValue) return query;

        return query.Where(partner => partner.Directions.Any(direction => direction.Id == directionId));
    }
}
