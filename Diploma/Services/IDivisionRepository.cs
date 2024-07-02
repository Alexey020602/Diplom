﻿using DataBase.Models;

namespace Diploma.Services;

public interface IDivisionRepository
{
    public Task<IEnumerable<Division>> GetDivisions(int? facultyId = null);
    public Task<Division> GetDivision(int id);
    public Task DeleteDivision(int id);
    public Task UpdateDivision(int id, Division division);
    public Task AddDivision(Division division);
}
