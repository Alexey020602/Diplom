using DataBase.Models;

namespace DataBase.Extensions;

public static class AgreementQuiryableFilterExtension
{
    public static IQueryable<Agreement> FilterByType(this IQueryable<Agreement> query, int? agreementTypeId)
    {
        if (!agreementTypeId.HasValue) return query;

        return query.Where(agreement => agreement.AgreementType.Id == agreementTypeId.Value);
    }

    public static IQueryable<Agreement> FilterBuStatus(this IQueryable<Agreement> query, int? agreementStatusId)
    {
        if (!agreementStatusId.HasValue) return query;

        return query.Where(agreement => agreement.AgreementStatus.Id == agreementStatusId.Value);
    }
}