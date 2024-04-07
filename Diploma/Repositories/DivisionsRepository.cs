using DataBase.Data;
using DataBase.Models;
using Diploma.Services;
using Microsoft.EntityFrameworkCore;
using Diploma.Extensions;

namespace Diploma.Repositories;

public class DivisionsRepository(ApplicationContext context): IDivisionRepository
{
    public Task AddDivision(Division division)
    {
        AttachDirections(division.Directions);
        if (division.Faculty != null) context.Faculties.Attach(division.Faculty);
        context.Divisions.Add(division);
        return context.SaveChangesAsync();
    }

    private void AttachDirections(IEnumerable<Direction> directions)
    {
        context.Directions.AttachRange(directions);
    }

    public async Task DeleteDivision(int id)
    {
        var division = await context.Divisions.FindAsync(id) 
            ?? throw new KeyNotFoundException("Не найдено подразделение");
        context.Divisions.Remove(division);
        await context.SaveChangesAsync();
    }

    public async Task<Division> GetDivision(int id) => await  context.Divisions.FindAsync(id)
        ?? throw new KeyNotFoundException("Не найдено подразделение");

    public async Task<IEnumerable<Division>> GetDivisions() => await context.Divisions.ToListAsync();

    public async Task UpdateDivision(int id, Division division)
    {
        var existingDivision = await context.Divisions.Include(d => d.Directions).Include(d => d.Faculty).FirstAsync(d => d.Id == id);
        /*?? throw new KeyNotFoundException("Не найдено подразделение")*/;

        context.Entry(existingDivision).CurrentValues.SetValues(division);

        existingDivision.Faculty = division.Faculty;
        existingDivision.Directions.UpdateByEnumerable(division.Directions);
        await context.SaveChangesAsync();
    }
}
