using Client.Services.BaseApi;
using Model.Agreements;
using Model.Interactions;
using Model.Partners;
using Refit;
using Partner = Model.Partners.Partner;

namespace Client.Services;

public interface IPartnersService: IReadOneApi<Partner, int>, IDeleteApi<int>, IUpdateApi<Partner, int>, ICreateApi<Partner>
{
    [Get("")] Task<List<PartnerShort>> ReadAll(int? partnerTypeId = null);
    [Get("/{id}/agreements")] Task<List<AgreementShort>> ReadAllAgreements(int id);
    [Get("/{id}/interactions")] Task<List<InteractionShort>> ReadAllInteractions(int id);
}
