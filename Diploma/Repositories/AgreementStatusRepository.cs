using DataBase.Data;
using DataBase.Models;
using Diploma.Services;
using Microsoft.EntityFrameworkCore;

namespace Diploma.Repositories;

public class AgreementStatusRepository(ApplicationContext context) : IAgreementStatusRepository
{
    public async Task AddAgreementsStatus(AgreementStatus agreementStatus)
    {
        await context.AgreementStatus.AddAsync(agreementStatus);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAgreementStatus(int id)
    {
       var existingAgreementStatus = context.AgreementStatus.Single(a => a.Id == id);
        context.AgreementStatus.Remove(existingAgreementStatus);
        await context.SaveChangesAsync();
    }

    public async Task<AgreementStatus> GetAgreementStatus(int id) => await context.AgreementStatus.FirstAsync(status => status.Id == id);

    public Task<List<AgreementStatus>> GetAgreementStatuses() => context.AgreementStatus.ToListAsync();

    public async Task UpdateAgreementStatus(int id, AgreementStatus agreementStatus)
    {
        var existingStatus = context.AgreementStatus.SingleAsync(status => status.Id == id);
        context.Entry(existingStatus).CurrentValues.SetValues(agreementStatus);
        await context.SaveChangesAsync();
    }
}
