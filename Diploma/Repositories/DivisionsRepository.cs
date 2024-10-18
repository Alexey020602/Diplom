using DataBase.Data;
using DataBase.Extensions;
using Diploma.Services;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.Divisions;
using Model.Extensions;
using Model.Mappers;
using Direction = DataBase.Models.Direction;
using Division = DataBase.Models.Division;
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

    public async Task<bool> CanDeleteDivision(int id)
    {
        
        var division = (await context.Divisions
            .Include(d => d.Interactions)
            .Include(d => d.DivisionsInAgreement)
            .FirstAsync(d => d.Id == id));
        
        return division.Interactions.Count == 0 && division.DivisionsInAgreement.Count == 0;
    }

    public Task<int> DivisionsCountAsync() => context.Divisions.CountAsync();

    public async Task<Paging<DivisionShort>> GetDivisions(DivisionsFilter filter)
    {
        var divisionsWithoutPaging = GetDivisionWithFaculty()
            .WhereWithNullable(filter.ShortName, shortName => (d => d.ShortName.Contains(shortName)))
            .WhereWithNullable(filter.FullName, fullName => (d => d.FullName.Contains(fullName)))
            .FilterByFaculty(filter.FacultyId)
            .OrderBy(d => d.Id);
        
        var take = filter.Take ?? 10;
        var skip = filter.Skip ?? 0;
        
        return new Paging<DivisionShort>(
                await divisionsWithoutPaging.CountAsync(),
                skip, take,
                await divisionsWithoutPaging
                    .Skip(skip)
                    .Take(take)
                    .Select(d => d.ConvertToDivisionShort())
                    .ToListAsync()
                );
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