using DataBase.Models;
using System.Net.Http.Json;

namespace Client.Services;

public class DirectionsService(HttpClient httpClient): IDirectionsService
{
    public async Task<List<Direction>> GetDirections()
    {
        var directions = await httpClient.GetFromJsonAsync<List<Direction>>("directions");

        return directions ?? throw new Exception("Список направлений оказался null");
    }

    public async Task<Direction> GetDirection(int id)
    {
        var direction = await httpClient.GetFromJsonAsync<Direction>("directions");

        return direction ?? throw new Exception("Пришло пустое значение направления");
    }

    public async Task DeleteDirection(int id) => await httpClient.DeleteAsync($"directions/{id}");
    public async Task UpdateDirection(Direction direction) => await httpClient.PostAsJsonAsync($"direction", direction);
    public async Task AddDirection(Direction direction) => await httpClient.PutAsJsonAsync($"direction", direction);
}
