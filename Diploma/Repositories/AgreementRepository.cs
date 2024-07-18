using System.Linq.Expressions;
using DataBase.Extensions;
using Diploma.Mappers;
using Model.Agreements;

namespace Diploma.Repositories;

using DataBase.Data;
using DataBase.Models;
using ModelAgreement = Model.Agreements.Agreement;
using Microsoft.EntityFrameworkCore;
using Services;
using System.Collections.Generic;
using System.Threading.Tasks;

public class AgreementRepository(ApplicationContext context): IAgreementRepository
{
    public async Task AddAgreement(ModelAgreement newAgreement)
    {
        var agreement = newAgreement.ConvertToDatabaseModel();
        context.Attach(agreement.AgreementStatus);
        context.Attach(agreement.AgreementType);
        // context.AttachRange(agreement.PartnerInAgreements);
        // context.AttachRange(agreement.DivisionInAgreements);
        context.Agreements.Add(agreement);
        await context.SaveChangesAsync();
    }
    public async Task DeleteAgreement(int id)
    {
        var agreement = await context.Agreements.FirstAsync(a => a.Id == id);
        context.Agreements.Remove(agreement);
        await context.SaveChangesAsync();
    }
    public async Task<ModelAgreement> GetAgreementById(int id) => (
        await context.Agreements
            .Include(a => a.AgreementStatus)
            .Include(a => a.AgreementType)
            .Include(a=>a.DivisionInAgreements)
            .ThenInclude(d => d.Division)
            .Include(a => a.PartnerInAgreements)
            .ThenInclude(p=>p.Partner)
            .FirstAsync(a => a.Id == id)
    ).ConvertToModel();

    public Task<List<AgreementShort>> GetAgreements(int? agreementTypeId, int? agreementStatusId) =>
        GetAgreementWithTypeAndStatus()
            .FilterByType(agreementTypeId)
            .FilterBuStatus(agreementStatusId)
            // .AsEnumerable()
            .Select(a => a.ConvertToShortModel())
            .ToListAsync();
    private IQueryable<Agreement> GetAgreementWithTypeAndStatus() => context.Agreements
        .AsNoTracking()
        .Include(x => x.AgreementType)
        .Include(x => x.AgreementStatus);
    public async Task UpdateAgreement(int id, ModelAgreement newAgreement)
    {
        var agreement = newAgreement.ConvertToDatabaseModel();
        var existingAgreement = await context.Agreements
            .Include(a => a.PartnerInAgreements)
            .Include(a => a.DivisionInAgreements)
            .FirstAsync(x => x.Id == id);

        context.Entry(existingAgreement).CurrentValues.SetValues(agreement);
        existingAgreement.AgreementStatus = agreement.AgreementStatus;
        existingAgreement.AgreementType = agreement.AgreementType;
        existingAgreement.DivisionInAgreements.UpdateByEnumerable(agreement.DivisionInAgreements);
        existingAgreement.PartnerInAgreements.UpdateByEnumerable(agreement.PartnerInAgreements);
        await context.SaveChangesAsync();
    }
}
