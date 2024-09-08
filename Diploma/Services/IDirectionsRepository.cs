// using DataBase.Models;

using Model;

namespace Diploma.Services;

public interface IDirectionsRepository
{
    public Task<IEnumerable<Direction>> GetDirections();
    public Task<Direction> GetDirection(int id);
    public Task AddDirection(Direction direction);
    public Task UpdateDirection(int id, Direction direction);
    public Task DeleteDirection(int id);
}