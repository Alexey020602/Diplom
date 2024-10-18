﻿using Client.Services.Api.BaseApi;
using Model;
using Model.Agreements;
using Model.Interactions;
using Model.Partners;
using Refit;
using Partner = Model.Partners.Partner;

namespace Client.Services.Api;

public interface IPartnersService : IReadApi<PartnerShort>, IReadOneApi<Partner, int>, IDeleteApi<int>,
    IUpdateApi<Partner, int>, ICreateApi<Partner>, ICountApi
{
    [Get("")]
    Task<Paging<PartnerShort>> ReadAll(
        PartnersFilter partnersFilter
        // string? shortName = null,
        // string? fullName = null,
        // int? partnerTypeId = null, 
        // int? directionId = null,
        // int? skip = null,
        // int? take = null
        );

    [Get("/{id}/agreements")]
    Task<List<AgreementShort>> ReadAllAgreements(int id);

    [Get("/{id}/interactions")]
    Task<List<InteractionShort>> ReadAllInteractions(int id);
}