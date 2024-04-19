﻿using Client.Services.BaseApi;
using DataBase.Models;
using Refit;
using SharedModel;
namespace Client.Services;

public interface IPartnersService: IReadOneApi<Partner, int>, IDeleteApi<int>, IUpdateApi<Partner, int>, ICreateApi<Partner>
{
    [Get("")] Task<List<PartnerShort>> ReadAll(int? partnerTypeId = null);
    [Get("/{id}/agreements")] Task<List<AgreementShort>> ReadAllAgreements(int id);
    [Get("/{id}/interactions")] Task<List<InteractionShort>> ReadAllInteractions(int id);
}
