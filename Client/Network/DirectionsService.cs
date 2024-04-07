using Client.Services;
using DataBase.Models;
using System.Net.Http.Json;

namespace Client.Network;

public class DirectionsService(HttpClient httpClient) : IDirectionsService
{
    public async Task<List<Direction>> ReadAll() => await httpClient.GetFromJsonAsync<List<Direction>>("directions") ?? 
            throw new Exception("Список направлений оказался null");

    public async Task<Direction> ReadOne(int id)
    {
        var direction = await httpClient.GetFromJsonAsync<Direction>("directions");

        return direction ?? throw new Exception("Пришло пустое значение направления");
    }

    public async Task Delete(int id) => await httpClient.DeleteAsync($"directions/{id}");
    public async Task Update(int id, Direction direction) => await httpClient.PutAsJsonAsync($"direction/{id}", direction);
    public async Task Create(Direction direction) => await httpClient.PostAsJsonAsync($"direction", direction);
}
