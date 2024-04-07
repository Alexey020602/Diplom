using DataBase.Models;

namespace Diploma.Services;

public interface IFacultyRepository
{
    public Task<IEnumerable<Faculty>> GetFaculties();
    public Task<Faculty> GetFaculty(int id);
    public Task AddFaculty(Faculty faculty);
    public Task UpdateFaculty(int id, Faculty faculty);
    public Task DeleteFaculty(int id);
}
