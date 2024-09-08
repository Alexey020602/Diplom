using Client.Services.Api;
using Client.Services.Api.BaseApi;
using Model.Agreements;
using Model.Partners;

namespace Client.Network;

public class PartnersForAgreementService(IReadApi<PartnerShort> api) : IPartnersForAgreementService
{
    public async Task<List<PartnerInAgreement>> GetPartners()
    {
        return (await api.ReadAll()).Select(ConvertFromShort).ToList();
    }

    private static PartnerInAgreement ConvertFromShort(PartnerShort partnerShort)
    {
        return new PartnerInAgreement
        {
            Id = partnerShort.Id,
            Name = partnerShort.Name
        };
    }
}