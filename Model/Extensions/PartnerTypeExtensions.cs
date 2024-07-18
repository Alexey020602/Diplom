using DataBase.Models;

namespace Model.Extensions;

public static class PartnerTypeExtensions
{
    public static Model.Partners.PartnerType ConvertToModel(this PartnerType partnerType) => new()
    {
        Id = partnerType.Id,
        Name = partnerType.Name,
    };

    public static PartnerType ConvertToDao(this Model.Partners.PartnerType partnerType) => new()
    {
        Id = partnerType.Id, Name = partnerType.Name,
    };
}