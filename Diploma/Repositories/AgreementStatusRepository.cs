using DataBase.Data;
using Diploma.Mappers;
using Diploma.Services;
using Microsoft.EntityFrameworkCore;
using Model.Agreements;

namespace Diploma.Repositories;

public class AgreementStatusRepository(ApplicationContext context) : IAgreementStatusRepository
{
    public async Task AddAgreementsStatus(Status agreementStatus)
    {
        await context.AgreementStatus.AddAsync(agreementStatus.ConvertToDatabaseModel());
        await context.SaveChangesAsync();
    }

    public async Task DeleteAgreementStatus(int id)
    {
        var existingAgreementStatus = context.AgreementStatus.Single(a => a.Id == id);
        context.AgreementStatus.Remove(existingAgreementStatus);
        await context.SaveChangesAsync();
    }

    public async Task<Status> GetAgreementStatus(int id)
    {
        return (await context.AgreementStatus.FirstAsync(status => status.Id == id)).ConvertToModel();
    }

    public Task<List<Status>> GetAgreementStatuses()
    {
        return context
            .AgreementStatus
            .Select(status => status.ConvertToModel())
            .ToListAsync();
    }

    public async Task UpdateAgreementStatus(int id, Status agreementStatus)
    {
        var existingStatus = context.AgreementStatus.SingleAsync(status => status.Id == id);
        context.Entry(existingStatus).CurrentValues.SetValues(agreementStatus.ConvertToDatabaseModel());
        await context.SaveChangesAsync();
    }
}