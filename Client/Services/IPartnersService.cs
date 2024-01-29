using DataBase.Models;
using SharedModel;
namespace Client.Services;

public interface IPartnersService
{
    Task<IEnumerable<PartnerForList>> GetPartners();
    Task<Partner> GetPartnerById(int id);

    Task DeletePartner(int id);
    Task AddPartner(Partner partner);
    Task UpdatePartner(Partner partner);
}
