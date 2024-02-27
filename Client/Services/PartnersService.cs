using DataBase.Models;
using System.Net.Http.Json;
using System.Collections.Specialized;
using System;

namespace Client.Services;
//TODO Сделать обработку кодов и исколючений
public class PartnersService(HttpClient httpClient) : IPartnersService
{
    public async Task AddPartner(Partner partner)
    {
        var result = await httpClient.PutAsJsonAsync("partners", partner);
        result.EnsureSuccessStatusCode();
    }

    public Task DeletePartner(int id) => httpClient.DeleteAsync($"partners/{id}");

    public async Task<Partner> GetPartnerById(int id)
    {
        var response = await httpClient.GetAsync($"partners/{id}");
        var partners = await response.Content.ReadFromJsonAsync<Partner>() ??
            throw new KeyNotFoundException("Partner not found");

        return partners;
    }

    public async Task<IEnumerable<Partner>> GetPartners(int? partnerTypeId) => await httpClient
        .GetFromJsonAsync<IEnumerable<Partner>>(CreateUriForPartners(partnerTypeId)) ?? 
            throw new Exception("Коллекция null почему-то");
        
    private static Uri CreateUriForPartners(int? partnerTypeId)
    {
        var query = string.Empty;
        if (partnerTypeId.HasValue)
        {
            query = "?" + $"{nameof(partnerTypeId)}={partnerTypeId.Value}";
        }
        return new Uri("partners" + query, UriKind.Relative);

    }

    public async Task UpdatePartner(Partner partner)
    {
        var result = await httpClient.PostAsJsonAsync("partners", partner);
        result.EnsureSuccessStatusCode();
    }
}
