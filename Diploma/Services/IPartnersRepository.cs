using DataBase.Models;

namespace Diploma.Services;

public interface IPartnersRepository
{
    Task AddPartnerAsync(Partner partner);
    Task<Partner> DeletePartnerByIdAsync(int id);
    Task UpdatePartnerAsync(Partner partner);
    Task<IEnumerable<Partner>> GetPartnersAsync();
    Task<Partner> GetPartnerByIdAsync(int id);
}