using DataBase.Models;
using System.Net.Http.Json;

namespace Client.Services;

public class PartnerTypesService(HttpClient httpClient): IPartnerTypesService
{
    public async Task AddPartnerType(PartnerType partnerType) => await httpClient.PutAsJsonAsync("partnerTypes", partnerType);
    
    public async Task DeletePartnerType(int id) => await httpClient.DeleteAsync($"partnerTypes/{id}");
    
    public async Task<PartnerType> GetPartnerTypeAsync(int id)
    {
        var partner = await httpClient.GetFromJsonAsync<PartnerType>($"partnerTypes/{id}");

        if (partner is null)
        {
            throw new KeyNotFoundException("Не найден тип партнера");
        }

        return partner;
    }

    public async Task<IEnumerable<PartnerType>> GetPartnerTypesAsync()
    {
        var partners = await httpClient.GetFromJsonAsync<IEnumerable<PartnerType>>("partnerTypes");

        if (partners is null)
        {
            throw new Exception("Пришел null вместо списка");
        }

        return partners;
    }

    public async Task UpdatePartnerType(PartnerType partnerType) => await httpClient.PostAsJsonAsync("partnerTypes", partnerType);
}
