using DataBase.Models;
using Refit;
namespace Client.Services;

public interface IDirectionsService
{
    [Get("")]
    Task<List<Direction>> ReadAll();
    [Get("/{id}")]
    Task<Direction> ReadOne(int id);
    [Delete("/{id}")]
    Task Delete(int id);
    [Put("/{id}")]
    Task Update(int id, Direction direction);
    [Post("")]
    Task Create(Direction direction);
}
