// using DataBase.Models;

using Model;
using Model.Partners;

namespace Diploma.Services;

public interface IPartnersRepository
{
    Task AddPartnerAsync(Partner partner);
    Task<Partner> DeletePartnerByIdAsync(int id);

    Task UpdatePartnerAsync(int id, Partner partner);

    // Task<IEnumerable<Partner>> GetPartnersAsync();
    Task<Paging<PartnerShort>> GetPartnersAsync(PartnersFilter filter);
    Task<Partner> GetPartnerByIdAsync(int id);
    Task<bool> CanDeletePartner(int id);
    Task<List<AgreementInPartner>> GetAgreementsForPartnerWithId(int id);
    Task<List<InteractionInPartner>> GetInteractionsForPartnerWithId(int id);
    Task<int> PartnersCountAsync();
}