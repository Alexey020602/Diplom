using DataBase.Models;
using System.Net.Http.Json;
using SharedModel;

namespace Client.Services;
//TODO Сделать обработку кодов и исколючений
public class PartnersService : IPartnersService
{
    private HttpClient httpClient;
    public PartnersService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }
    public async Task AddPartner(Partner partner)
    {
        var result = await httpClient.PutAsJsonAsync("partners", partner);

        result.EnsureSuccessStatusCode();
    }

    public async Task DeletePartner(int id)
    {
        await httpClient.DeleteAsync($"partners/{id}");
    }

    public async Task<Partner> GetPartnerById(int id)
    {
        var response = await httpClient.GetAsync($"partners/{id}");
        var partners = await response.Content.ReadFromJsonAsync<Partner>();

        if (partners is null)
        {
            throw new KeyNotFoundException("Partner not found");
        }

        return partners;
    }

    public async Task<IEnumerable<PartnerForList>> GetPartners()
    {
        var partners = await httpClient.GetFromJsonAsync< IEnumerable<PartnerForList> >("partners");

        if (partners is null)
        {
            //TODO Добавить выбрасывание исключения
            throw new Exception("Коллекция null почему-то");
        }

        return partners;
    }

    public async Task UpdatePartner(Partner partner)
    {
        var result = await httpClient.PostAsJsonAsync("partners", partner);
        result.EnsureSuccessStatusCode();
    }
}
