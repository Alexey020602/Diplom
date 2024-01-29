using DataBase.Data;
using DataBase.Models;
using Microsoft.EntityFrameworkCore;

namespace Diploma.Services;

public class DirectionsRepository(ApplicationContext context): IDirectionsRepository
{
    public async Task<IEnumerable<Direction>> GetDirections() => await context.Directions.ToListAsync();
    public async Task<Direction> GetDirection(int  id)
    {
        var direction = await context.Directions.FindAsync(id);

        return direction ?? throw new KeyNotFoundException("Не найдено направление");
    }

    public async Task UpdateDirection(Direction direction)
    {
        context.Directions.Update(direction);
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
