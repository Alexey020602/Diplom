using DataBase.Data;
using DataBase.Models;

namespace Client.Services;

public interface IPartnerTypesService
{
    Task<IEnumerable<PartnerType>> GetPartnerTypesAsync();
    Task<PartnerType> GetPartnerTypeAsync(int id);
    Task UpdatePartnerType(PartnerType partnerType);
    Task AddPartnerType(PartnerType partnerType);
    Task DeletePartnerType(int id);
}
