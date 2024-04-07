using Client.Services;
using DataBase.Models;
using System.Net.Http.Json;

namespace Client.Network;

public class PartnerTypesService(HttpClient httpClient) : IPartnerTypesService
{
    public async Task Create(PartnerType partnerType) => await httpClient.PostAsJsonAsync("partnerTypes", partnerType);

    public async Task Delete(int id) => await httpClient.DeleteAsync($"partnerTypes/{id}");

    public async Task<PartnerType> ReadOne(int id) => await httpClient.GetFromJsonAsync<PartnerType>($"partnerTypes/{id}") ?? 
        throw new KeyNotFoundException("Не найден тип партнера");


    public async Task<List<PartnerType>> ReadAll() => await httpClient.GetFromJsonAsync<List<PartnerType>>("partnerTypes") ??
        throw new Exception("Пришел null вместо списка");

    public async Task Update(int id, PartnerType partnerType) => await httpClient.PutAsJsonAsync($"partnerTypes/{id}", partnerType);
}
