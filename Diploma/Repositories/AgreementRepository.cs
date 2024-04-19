namespace Diploma.Repositories;

using DataBase.Data;
using DataBase.Models;
using Diploma.Extensions;
using Microsoft.EntityFrameworkCore;
using Services;
using System.Collections.Generic;
using System.Threading.Tasks;

public class AgreementRepository(ApplicationContext context): IAgreementRepository
{
    public async Task AddAgreement(Agreement agreement)
    {
        context.AttachRange(agreement.PartnerInAgreements);
        context.AttachRange(agreement.DivisionInAgreements);
        context.Agreements.Add(agreement);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAgreement(int id)
    {
        var agreement = await context.Agreements.FirstAsync(a => a.Id == id);
        context.Agreements.Remove(agreement);
        await context.SaveChangesAsync();
    }

    public Task<Agreement> GetAgreementById(int id) => context.Agreements
        .Include(a => a.AgreementStatus)
        .Include(a => a.AgreementType)
        .FirstAsync(a => a.Id == id);
    public async Task<IEnumerable<Agreement>> GetAgreements(int? agreementTypeId, int? agreementStatusId) => 
        await GetAgreementWithTypeAndStatus()
        .FilterByType(agreementTypeId)
        .FilterBuStatus(agreementStatusId)
        .ToListAsync();
    private IQueryable<Agreement> GetAgreementWithTypeAndStatus() => context.Agreements
        .Include(x => x.AgreementType).
        Include(x => x.AgreementStatus);
    public async Task UpdateAgreement(int id, Agreement agreement)
    {
        var existingAgreement = await context.Agreements
            .Include(a => a.PartnerInAgreements)
            .Include(a => a.DivisionInAgreements)
            .FirstAsync(x => x.Id == id);

        context.Entry(existingAgreement).CurrentValues.SetValues(agreement);
        existingAgreement.AgreementStatus = agreement.AgreementStatus;
        existingAgreement.AgreementType = agreement.AgreementType;
        existingAgreement.DivisionInAgreements.UpdateByEnumerable(agreement.DivisionInAgreements);

        await context.SaveChangesAsync();
    }
}
