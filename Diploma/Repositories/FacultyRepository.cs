using DataBase.Data;
using DataBase.Models;
using Diploma.Services;
using Microsoft.EntityFrameworkCore;

namespace Diploma.Repositories;

public class FacultyRepository(ApplicationContext context) : IFacultyRepository
{
    public async Task AddFaculty(Faculty faculty)
    {
        context.Faculties.Add(faculty);
        await context.SaveChangesAsync();
    }

    public Task DeleteFaculty(int id)
    {
        var faculty = context.Faculties.Find(id) ??
            throw new KeyNotFoundException("Не найден факультет");

        context.Faculties.Remove(faculty);
        return context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Faculty>> GetFaculties() => await context.Faculties.ToListAsync();

    public async Task<Faculty> GetFaculty(int id) =>
        await context.Faculties.FirstOrDefaultAsync(faculty => faculty.Id == id) ??
        throw new KeyNotFoundException("Не найден факультет");


    public async Task UpdateFaculty(int id, Faculty faculty)
    {
        var existedFaculty = await context.Faculties.FirstAsync(f => f.Id == id);
        context.Faculties.Entry(existedFaculty).CurrentValues.SetValues(faculty);
        await context.SaveChangesAsync();
    }
}
