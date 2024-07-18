using DataBase.Data;
using DBDirection = DataBase.Models.Direction;
using Model;
using Diploma.Services;
using Microsoft.EntityFrameworkCore;
using Model.Extensions;

namespace Diploma.Repositories;

public class DirectionsRepository(ApplicationContext context) : IDirectionsRepository
{
    public async Task<IEnumerable<Direction>> GetDirections() => await context
        .Directions
        .Select(d => d.ConvertToModel())
        .ToListAsync();

    public async Task<Direction> GetDirection(int id) =>
        (await context.Directions.FirstAsync(d => d.Id == id)).ConvertToModel();
        // ?? throw new KeyNotFoundException("Не найдено направление");

    public async Task UpdateDirection(int id, Direction direction)
    {
        var existingDirection = await context.Directions.FindAsync(id) ??
            throw new KeyNotFoundException("Не найдено направление");

        context.Entry(existingDirection).CurrentValues.SetValues(direction.ConvertToDao());
        await context.SaveChangesAsync();
    }

    public async Task AddDirection(Direction direction)
    {
        context.Directions.Add(direction.ConvertToDao());
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
