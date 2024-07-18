using DataBase.Data;
using DataBase.Models;
using Diploma.Mappers;
using Diploma.Services;
using Microsoft.EntityFrameworkCore;
using AgreementType = Model.Agreements.AgreementType;

namespace Diploma.Repositories;

public class AgreementTypeRepository(ApplicationContext context) : IAgreementTypeRepository
{
    public async Task AddAgreementType(AgreementType agreementAgreementType)
    {
        await context.AgreementType.AddAsync(agreementAgreementType.ConvertToDatabaseModel());
        await context.SaveChangesAsync();
    }

    public async Task DeleteAgreementType(int id)
    {
        var existingAgreementType = await context.AgreementType.SingleAsync(a => a.Id == id);
        context.AgreementType.Remove(existingAgreementType);
        await context.SaveChangesAsync();
    }

    public async Task<AgreementType> GetAgreementType(int id) => 
        (await context.AgreementType.SingleAsync(a => a.Id == id)).ConvertToModel();

    public Task<List<AgreementType>> GetAgreementTypes() => 
        context.AgreementType.Select(type => type.ConvertToModel()).ToListAsync();

    public async Task UpdateAgreementType(int id, AgreementType agreementAgreementType)
    {
        var existingAgreementType = await context.AgreementType.SingleAsync(a => a.Id == id);
        context.Entry(existingAgreementType).CurrentValues.SetValues(agreementAgreementType.ConvertToDatabaseModel());
        await context.SaveChangesAsync();
    }
}
