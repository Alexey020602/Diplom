using DataBase.Models;
using System.Net.Http.Json;
using System.Collections.Specialized;
using System;
using Client.Services;

namespace Client.Network;
//TODO Сделать обработку кодов и исколючений
public class PartnersService(HttpClient httpClient) : IPartnersService
{
    private const string basePath = "partners/";
    public async Task AddPartner(Partner partner)
    {
        var result = await httpClient.PutAsJsonAsync(basePath, partner);
        result.EnsureSuccessStatusCode();
    }
    private string CreatePathWithId(int id) => $"{basePath}{id}";

    public Task DeletePartner(int id) => httpClient.DeleteAsync(CreatePathWithId(id));

    public async Task<Partner> GetPartnerById(int id)
    {
        var response = await httpClient.GetAsync(CreatePathWithId(id));
        var partners = await response.Content.ReadFromJsonAsync<Partner>() ??
            throw new KeyNotFoundException("Partner not found");

        return partners;
    }

    public async Task<IEnumerable<Partner>> GetPartners(int? partnerTypeId)
    {
        Console.WriteLine("Загрузка спика партнеров");
        Console.WriteLine(httpClient.BaseAddress);

        return await httpClient
        .GetFromJsonAsync<IEnumerable<Partner>>(CreateUriForPartners(partnerTypeId)) ??
            throw new Exception("Коллекция null почему-то");
    }


    private static string CreateUriForPartners(int? partnerTypeId)
    {
        var query = string.Empty;
        if (partnerTypeId.HasValue)
        {
            query = "?" + $"{nameof(partnerTypeId)}={partnerTypeId.Value}";
        }
        return basePath + query;

    }

    public async Task UpdatePartner(Partner partner)
    {
        var result = await httpClient.PostAsJsonAsync(basePath, partner);
        result.EnsureSuccessStatusCode();
    }
}
