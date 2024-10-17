using DataBase.Data;
using DataBase.Extensions;
using Diploma.Services;
using Microsoft.EntityFrameworkCore;
using Model.Agreements;
using Model.Mappers;

namespace Diploma.Repositories;

using ModelAgreement = Agreement;

public class AgreementRepository(ApplicationContext context) : IAgreementRepository
{
    
    public async Task AddAgreement(ModelAgreement newAgreement)
    {
        var agreement = newAgreement.ConvertToDatabaseModel();
        AttachEntity(agreement);
        context.Agreements.Add(agreement);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAgreement(int id)
    {
        var agreement = await context.Agreements.FirstAsync(a => a.Id == id);
        context.Agreements.Remove(agreement);
        await context.SaveChangesAsync();
    }

    public async Task<ModelAgreement> GetAgreementById(int id)
    {
        return (
            await context.Agreements
                .Include(a => a.AgreementStatus)
                .Include(a => a.AgreementType)
                .Include(a => a.DivisionInAgreements)
                .ThenInclude(d => d.Division)
                .Include(a => a.PartnerInAgreements)
                .ThenInclude(p => p.Partner)
                .FirstAsync(a => a.Id == id)
        ).ConvertToModel();
    }

    public Task<List<AgreementShort>> GetAgreements(
        string? number,
        int? agreementTypeId,
        int? agreementStatusId, 
        DateOnly? startDate = null, 
        DateOnly? endDate = null,
        int skip = 0,
        int take = 10)
    {
        return GetAgreementWithTypeAndStatus()
            .FilterByDate(startDate, a => a.StarDateTime)
            .FilterByDate(endDate, a => a.EndDateTime)
            .FilterByName(number, agreement => agreement.AgreementNumber)
            .FilterByType(agreementTypeId)
            .FilterBuStatus(agreementStatusId)
            .OrderBy(a => a.Id)
            .Skip(skip)
            .Take(take)
            // .AsEnumerable()
            .Select(a => a.ConvertToShortModel())
            .ToListAsync();
    }

    public async Task UpdateAgreement(int id, ModelAgreement newAgreement)
    {
        var agreement = newAgreement.ConvertToDatabaseModel();
        AttachEntity(agreement);
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

    private void AttachEntity(DataBase.Models.Agreement agreement)
    {
        context.DivisionsInAgreement.AttachRange(agreement.DivisionInAgreements);
        context.PartnersInAgreement.AttachRange(agreement.PartnerInAgreements);
        context.AgreementType.Attach(agreement.AgreementType);
        context.AgreementStatus.Attach(agreement.AgreementStatus);
    }
    private IQueryable<DataBase.Models.Agreement> GetAgreementWithTypeAndStatus()
    {
        return context.Agreements
            .AsNoTracking()
            .Include(x => x.AgreementType)
            .Include(x => x.AgreementStatus);
    }
}