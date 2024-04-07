using DataBase.Models;

namespace Diploma.Services;

public interface IPartnerTypesRepository
{
    Task AddPartnerTypeAsync(PartnerType partnerType);
    Task DeletePartnerTypeAsync(PartnerType partnerType);
    Task DeletePartnerTypeByIdAsync(int id);
    Task<IEnumerable<PartnerType>> GetAllPartnerTypesAsync();
    Task<PartnerType> GetPartnerTypeByIdAsync(int id);
    Task UpdatePartnerTypeAsync(int id, PartnerType partnerType);
}
