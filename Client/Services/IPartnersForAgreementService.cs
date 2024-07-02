using Model.Agreements;

namespace Client.Services;

public interface IPartnersForAgreementService
{
    Task<List<Partner>> GetPartners();
}