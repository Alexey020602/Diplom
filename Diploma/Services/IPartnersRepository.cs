using DataBase.Models;

namespace Diploma.Services;

public interface IPartnersRepository
{
    Task AddPartnerAsync(Partner partner);
    Task<Partner> DeletePartnerByIdAsync(int id);
    Task UpdatePartnerAsync(int id, Partner partner);
    Task<IEnumerable<Partner>> GetPartnersAsync();
    Task<IEnumerable<Partner>> GetPartnersAsync(int? partnerTypeId, int? directionId);
    Task<Partner> GetPartnerByIdAsync(int id);
}