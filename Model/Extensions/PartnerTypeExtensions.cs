using Model.Partners;

namespace Model.Extensions;

public static class PartnerTypeExtensions
{
    public static PartnerType ConvertToModel(this DataBase.Models.PartnerType partnerType)
    {
        return new PartnerType
        {
            Id = partnerType.Id,
            Name = partnerType.Name
        };
    }

    public static DataBase.Models.PartnerType ConvertToDao(this PartnerType partnerType)
    {
        return new DataBase.Models.PartnerType
        {
            Id = partnerType.Id, Name = partnerType.Name
        };
    }
}