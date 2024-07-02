using Client.Services;
using Client.Services.BaseApi;
using Model.Partners;
using Partner = Model.Agreements.Partner;

namespace Client.Network;

public class PartnersForAgreementService(IReadApi<PartnerShort> api) : IPartnersForAgreementService
{
    public async Task<List<Partner>> GetPartners() => (await api.ReadAll()).Select(ConvertFromShort).ToList();

    private static Partner ConvertFromShort(PartnerShort partnerShort) => new()
    {
        Id = partnerShort.Id,
        Name = partnerShort.Name,
    };
}