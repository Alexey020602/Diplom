using Model.Agreements;

namespace Client.Services.Api;

public interface IPartnersForAgreementService
{
    Task<List<PartnerInAgreement>> GetPartners();
}