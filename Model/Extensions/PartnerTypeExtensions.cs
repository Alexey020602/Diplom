using DataBase.Models;

namespace Diploma.Extensions.ModelToDao;

public static class PartnerTypeExtensions
{
    public static Model.Partners.Type ConvertToModel(this PartnerType partnerType) => new()
    {
        Id = partnerType.Id,
        Name = partnerType.Name,
    };

    public static PartnerType ConvertToDao(this Model.Partners.Type type) => new()
    {
        Id = type.Id, Name = type.Name,
    };
}