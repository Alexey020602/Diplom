using DataBase.Data;
using DataBase.Models;
using ModelFaculty = Model.Divisions.Faculty;
using Diploma.Services;
using Microsoft.EntityFrameworkCore;
using Model.Extensions;

namespace Diploma.Repositories;

public class FacultyRepository(ApplicationContext context) : IFacultyRepository
{
    public async Task AddFaculty(ModelFaculty newFaculty)
    {
        var faculty = newFaculty.ToDao();
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

    public async Task<IEnumerable<ModelFaculty>> GetFaculties() => await context.Faculties.Select(f => f.ToModel()).ToListAsync();

    public async Task<ModelFaculty> GetFaculty(int id) =>
        await context.Faculties.Select(f => f.ToModel()).FirstOrDefaultAsync(faculty => faculty.Id == id) ??
        throw new KeyNotFoundException("Не найден факультет");


    public async Task UpdateFaculty(int id, ModelFaculty newFaculty)
    {
        var faculty = newFaculty.ToDao();
        var existedFaculty = await context.Faculties.FirstAsync(f => f.Id == id);
        context.Faculties.Entry(existedFaculty).CurrentValues.SetValues(faculty);
        await context.SaveChangesAsync();
    }
}
