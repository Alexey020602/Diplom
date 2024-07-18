// using DataBase.Models;
using Model.Partners;
namespace Diploma.Services;

public interface IPartnersRepository
{
    Task AddPartnerAsync(Partner partner);
    Task<Partner> DeletePartnerByIdAsync(int id);
    Task UpdatePartnerAsync(int id, Partner partner);
    // Task<IEnumerable<Partner>> GetPartnersAsync();
    Task<IEnumerable<Partner>> GetPartnersAsync(int? partnerTypeId = null, int? directionId = null);
    Task<Partner> GetPartnerByIdAsync(int id);
    Task<List<AgreementInPartner>> GetAgreementsForPartnerWithId(int id);
    Task<List<InteractionInPartner>> GetInteractionsForPartnerWithId(int id);
}