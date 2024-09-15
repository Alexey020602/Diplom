// using DataBase.Models;

using Model.Partners;

namespace Diploma.Services;

public interface IPartnerTypesRepository
{
    Task AddPartnerTypeAsync(PartnerType partnerPartnerType);
    Task DeletePartnerTypeAsync(PartnerType partnerPartnerType);
    Task DeletePartnerTypeByIdAsync(int id);
    Task<IEnumerable<PartnerType>> GetAllPartnerTypesAsync();
    Task<PartnerType> GetPartnerTypeByIdAsync(int id);
    Task UpdatePartnerTypeAsync(int id, PartnerType partnerPartnerType);
}