using DataBase.Models;

namespace DataBase.Extensions;

public static class InteractionQuiryableFilterExtensions
{
    public static IQueryable<Interaction> FilterByType(this IQueryable<Interaction> source, int? interactionTypeId)
    {
        if (!interactionTypeId.HasValue) return source;

        return source.Where(i => i.InteractionType.Id == interactionTypeId.Value);
    }
}
