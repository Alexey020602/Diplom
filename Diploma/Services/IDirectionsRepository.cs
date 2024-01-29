using DataBase.Models;

namespace Diploma.Services;

public interface IDirectionsRepository
{
    public Task<IEnumerable<Direction>> GetDirections();
    public Task<Direction> GetDirection(int id);
    public Task AddDirection(Direction direction);
    public Task UpdateDirection(Direction direction);
    public Task DeleteDirection(int id);
}
