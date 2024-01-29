using DataBase.Models;

namespace Diploma.Services;

public interface IDivisionRepository
{
    public Task<IEnumerable<Division>> GetDivisions();
    public Task<Division> GetDivision(int id);
    public Task DeleteDivision(int id);
    public Task UpdateDivision(Division division);
    public Task AddDivision(Division division);
}
