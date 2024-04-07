using DataBase.Models;
using System.Net.Http.Json;
using System.Collections.Specialized;
using System;
using Client.Services;
using SharedModel;

namespace Client.Network;
//TODO Сделать обработку кодов и исколючений
public class PartnersService(HttpClient httpClient) : IPartnersService
{
    private const string basePath = "partners/";
    public async Task Create(Partner partner)
    {
        var result = await httpClient.PostAsJsonAsync(basePath, partner);
        result.EnsureSuccessStatusCode();
    }
    private static string CreatePathWithId(int id) => $"{basePath}{id}";

    public Task Delete(int id) => httpClient.DeleteAsync(CreatePathWithId(id));

    public async Task<Partner> ReadOne(int id)
    {
        var response = await httpClient.GetAsync(CreatePathWithId(id));
        var partners = await response.Content.ReadFromJsonAsync<Partner>() ??
            throw new KeyNotFoundException("Partner not found");

        return partners;
    }

    public async Task<List<PartnerShort>> ReadAll(int? partnerTypeId)
    {
        Console.WriteLine("Загрузка спика партнеров");
        Console.WriteLine(httpClient.BaseAddress);

        return await httpClient
        .GetFromJsonAsync<List<PartnerShort>>(CreateUriForPartners(partnerTypeId)) ??
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

    public async Task Update(int id, Partner partner)
    {
        var result = await httpClient.PutAsJsonAsync($"{basePath}{id}", partner);
        result.EnsureSuccessStatusCode();
    }
}
