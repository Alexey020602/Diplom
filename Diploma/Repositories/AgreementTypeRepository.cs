using DataBase.Data;
using DataBase.Models;
using Diploma.Services;
using Microsoft.EntityFrameworkCore;

namespace Diploma.Repositories;

public class AgreementTypeRepository(ApplicationContext context) : IAgreementTypeRepository
{
    public async Task AddAgreementType(AgreementType agreementType)
    {
        await context.AgreementType.AddAsync(agreementType);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAgreementType(int id)
    {
        var existingAgreementType = await context.AgreementType.SingleAsync(a => a.Id == id);
        context.AgreementType.Remove(existingAgreementType);
        await context.SaveChangesAsync();
    }

    public Task<AgreementType> GetAgreementType(int id) => context.AgreementType.SingleAsync(a => a.Id == id);

    public Task<List<AgreementType>> GetAgreementTypes() => context.AgreementType.ToListAsync();

    public async Task UpdateAgreementType(int id, AgreementType agreementType)
    {
        var existingAgreementType = await context.AgreementType.SingleAsync(a => a.Id == id);
        context.Entry(existingAgreementType).CurrentValues.SetValues(agreementType);
        await context.SaveChangesAsync();
    }
}
