using DataBase.Models;
using Refit;
namespace Client.Services;

public interface IDirectionsService
{
    [Get("")]
    Task<IEnumerable<Direction>> GetDirections();
    [Get("/{id}")]
    Task<Direction> GetDirection(int id);
    [Delete("/{id}")]
    Task DeleteDirection(int id);
    [Post("")]
    Task UpdateDirection(Direction direction);
    [Put("")]
    Task AddDirection(Direction direction);
}
