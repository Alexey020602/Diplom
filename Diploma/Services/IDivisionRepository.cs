// using DataBase.Models;

using Model.Divisions;

namespace Diploma.Services;

public interface IDivisionRepository
{
    public Task<IEnumerable<Division>> GetDivisions(string? shortName = null, string? fullName = null,  int? facultyId = null);
    public Task<Division> GetDivision(int id);
    public Task<bool> CanDeleteDivision(int id);
    public Task DeleteDivision(int id);
    public Task UpdateDivision(int id, Division division);
    public Task AddDivision(Division division);
}