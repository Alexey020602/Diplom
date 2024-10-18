// using DataBase.Models;

using Model;
using Model.Divisions;

namespace Diploma.Services;

public interface IDivisionRepository
{
    public Task<Paging<DivisionShort>> GetDivisions(DivisionsFilter filter);
    public Task<Division> GetDivision(int id);
    public Task<bool> CanDeleteDivision(int id);
    public Task DeleteDivision(int id);
    public Task UpdateDivision(int id, Division division);
    public Task AddDivision(Division division);
    public Task<int> DivisionsCountAsync();
}