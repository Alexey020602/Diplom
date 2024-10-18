﻿using DataBase.Data;
using DataBase.Extensions;
using Diploma.Services;
using Microsoft.EntityFrameworkCore;
using Model;
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

    public Task<int> CountAgreements() => context.Agreements.CountAsync();

    public async Task<Paging<AgreementShort>> GetAgreements(AgreementsFilter filter)
    {
        var startDate = filter.StartDate?.ToDateTime(new());
        var endDate = filter.EndDate?.ToDateTime(new());
        var agreementsWithoutPagging = GetAgreementWithTypeAndStatus()
            .WhereWithNullable(startDate, startDate => (a => a.StarDateTime.Date == startDate))
            .WhereWithNullable(endDate, endDate => (a => a.EndDateTime.Date == endDate))
            .WhereWithNullable(filter.Number, number => (a => a.AgreementNumber.Contains(number)))
            .FilterByType(filter.AgreementTypeId)
            .FilterBuStatus(filter.AgreementStatusId)
            .OrderBy(a => a.Id);
        var take = filter.Take ?? 10;
        var skip = filter.Skip ?? 0;
        
        return new Paging<AgreementShort>(
                await agreementsWithoutPagging.CountAsync(),
                skip,
                take,
                await agreementsWithoutPagging
                    .Skip(skip)
                    .Take(take)
                    .Select(a => a.ConvertToShortModel())
                    .ToListAsync()
                )
            ;
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