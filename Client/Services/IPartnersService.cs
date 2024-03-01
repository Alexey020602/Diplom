using DataBase.Models;
using Refit;
using SharedModel;
namespace Client.Services;

public interface IPartnersService
{
    [Get("")]
    Task<IEnumerable<Partner>> GetPartners(int? partnerTypeId = null);
    [Get("/{id}")]
    Task<Partner> GetPartnerById(int id);
    [Delete("/{id}")]
    Task DeletePartner(int id);
    [Put("")]
    Task AddPartner(Partner partner);
    [Post("")]
    Task UpdatePartner(Partner partner);
}
