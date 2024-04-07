using DataBase.Data;
using DataBase.Models;
using Diploma.Services;
using Microsoft.EntityFrameworkCore;

namespace Diploma.Repositories;

public class DirectionsRepository(ApplicationContext context) : IDirectionsRepository
{
    public async Task<IEnumerable<Direction>> GetDirections() => await context.Directions.ToListAsync();
    public async Task<Direction> GetDirection(int id)
    {
        var direction = await context.Directions.FindAsync(id);

        return direction ?? throw new KeyNotFoundException("Не найдено направление");
    }

    public async Task UpdateDirection(int id, Direction direction)
    {
        var existingDirection = await context.Directions.FindAsync(id) ??
            throw new KeyNotFoundException("Не найдено направление");

        context.Entry(existingDirection).CurrentValues.SetValues(direction);
        await context.SaveChangesAsync();
    }

    public async Task AddDirection(Direction direction)
    {
        context.Directions.Add(direction);
        await context.SaveChangesAsync();
    }

    public async Task DeleteDirection(int id)
    {
        var direction = await context.Directions.FindAsync(id) ??
            throw new KeyNotFoundException("Не найдено направление");

        context.Directions.Remove(direction);
        await context.SaveChangesAsync();
    }
}
