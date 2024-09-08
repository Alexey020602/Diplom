using DataBase.Data;
using DataBase.Extensions;
using DataBase.Models;
using Diploma.Services;
using Microsoft.EntityFrameworkCore;
using Model.Extensions;
using ModelDivision = Model.Divisions.Division;

namespace Diploma.Repositories;

public class DivisionsRepository(ApplicationContext context) : IDivisionRepository
{
    public Task AddDivision(ModelDivision newDivision)
    {
        var division = newDivision.ToDao();
        AttachDirections(division.Directions);
        if (division.Faculty != null) context.Faculties.Attach(division.Faculty);
        context.Divisions.Add(division);
        return context.SaveChangesAsync();
    }

    public async Task DeleteDivision(int id)
    {
        var division = await context.Divisions
                           .FindAsync(id)
                       ?? throw new KeyNotFoundException("Не найдено подразделение");
        context.Divisions.Remove(division);
        await context.SaveChangesAsync();
    }

    public async Task<ModelDivision> GetDivision(int id)
    {
        return (await context.Divisions
            .Include(d => d.Faculty)
            .Include(d => d.Directions)
            .FirstAsync(d => d.Id == id)).ToModel();
    }

    public async Task<IEnumerable<ModelDivision>> GetDivisions(int? facultyId)
    {
        return await GetDivisionWithFaculty()
            .FilterByFaculty(facultyId)
            .Select(d => d.ToModel())
            .ToListAsync();
    }

    public async Task UpdateDivision(int id, ModelDivision newDivision)
    {
        var division = newDivision.ToDao();
        var existingDivision = await context.Divisions.Include(d => d.Directions).FirstAsync(d => d.Id == id);
        /*?? throw new KeyNotFoundException("Не найдено подразделение")*/
        ;

        context.Entry(existingDivision).CurrentValues.SetValues(division);

        existingDivision.Faculty = division.Faculty;
        existingDivision.Directions.UpdateByEnumerable(division.Directions);
        await context.SaveChangesAsync();
    }

    private void AttachDirections(IEnumerable<Direction> directions)
    {
        context.Directions.AttachRange(directions);
    }

    private IQueryable<Division> GetDivisionWithFaculty()
    {
        return context.Divisions
            .Include(d => d.Faculty);
    }
}