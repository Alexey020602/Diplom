using Client.Services;
using Client.Services.BaseApi;
using Model.Agreements;
using Model.Partners;

namespace Client.Network;

public class PartnersForAgreementService(IReadApi<PartnerShort> api) : IPartnersForAgreementService
{
    public async Task<List<PartnerInAgreement>> GetPartners() => (await api.ReadAll()).Select(ConvertFromShort).ToList();

    private static PartnerInAgreement ConvertFromShort(PartnerShort partnerShort) => new()
    {
        Id = partnerShort.Id,
        Name = partnerShort.Name,
    };
}