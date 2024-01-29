using DataBase.Data;
using DataBase.Models;
using Microsoft.EntityFrameworkCore;

namespace Diploma.Services;

public class FacultyRepository(ApplicationContext context) : IFacultyRepository
{
    public async Task AddFaculty(Faculty faculty)
    {
        context.Faculties.Add(faculty);
        await context.SaveChangesAsync();
    }

    public async Task DeleteFaculty(int id)
    {
        var faculty = await context.Faculties.FirstOrDefaultAsync(x => x.Id == id) ?? 
            throw new KeyNotFoundException("Не найден факультет");

        context.Faculties.Remove(faculty);
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Faculty>> GetFaculties() => await context.Faculties.ToListAsync();

    public async Task<Faculty> GetFaculty(int id) => 
        await context.Faculties.FirstOrDefaultAsync(faculty => faculty.Id == id) ?? 
        throw new KeyNotFoundException("Не найден факультет");
    

    public async Task UpdateFaculty(Faculty faculty)
    {
        context.Faculties.Update(faculty);
        await context.SaveChangesAsync();
    }
}
