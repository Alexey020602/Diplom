using Diploma.Extensions;
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
        var agreement = ConvertToDatabaseModel(newAgreement);
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
    public async Task<ModelAgreement> GetAgreementById(int id) => ConvertToModel(
        await context.Agreements
            .Include(a => a.AgreementStatus)
            .Include(a => a.AgreementType)
            .FirstAsync(a => a.Id == id)
    );
    public async Task<IEnumerable<AgreementShort>> GetAgreements(int? agreementTypeId, int? agreementStatusId) => 
        await GetAgreementWithTypeAndStatus()
        .FilterByType(agreementTypeId)
        .FilterBuStatus(agreementStatusId)
        .Select(a => new AgreementShort(a.Id, a.ToString()))
        .ToListAsync();
    private IQueryable<Agreement> GetAgreementWithTypeAndStatus() => context.Agreements
        .Include(x => x.AgreementType)
        .Include(x => x.AgreementStatus);
    public async Task UpdateAgreement(int id, ModelAgreement newAgreement)
    {
        var agreement = ConvertToDatabaseModel(newAgreement);
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
    private Agreement ConvertToDatabaseModel(ModelAgreement agreement) => new()
    {
        Id  = agreement.Id,
        AgreementNumber = agreement.Number,
        AgreementType = ConvertToDatabaseModel(agreement.Type!),
        StarDateTime = agreement.Start,
        EndDateTime = agreement.End,
        DivisionInAgreements = ConvertToDatabaseModel(agreement.Divisions, agreement.Id),
        PartnerInAgreements = ConvertToDatabaseModel(agreement.Partners, agreement.Id),
    };
    private AgreementType ConvertToDatabaseModel(Model.Agreements.Type type)
    {
        var newType = new AgreementType()
        {
            Id = type.Id,
            Name = type.Name,
        };
        context.Attach(newType);
        return newType;
    }
    private AgreementStatus ConvertToDatabaseModel(Model.Agreements.Status status)
    {
        var newStatus = new AgreementStatus()
        {
            Id = status.Id,
            Name = status.Name,
        };
        context.Attach(newStatus);
        return newStatus;
    }
    private List<DivisionInAgreement> ConvertToDatabaseModel(
        IEnumerable<Model.Agreements.Division> newDivisions,
        int agreementId) =>
        newDivisions
            .Select(d => ConvertToDatabaseModel(d, agreementId))
            .ToList();
    private DivisionInAgreement ConvertToDatabaseModel(Model.Agreements.Division newDivision, int agreementId)
    {
        var division = new DivisionInAgreement()
        {
            AgreementId = agreementId,
            DivisionInAgreementId = newDivision.Id,
            ContactPersons = newDivision.ContactPersons,
        };
        context.Attach(division);
        return division;
    }
    private List<PartnerInAgreement> ConvertToDatabaseModel(IEnumerable<Model.Agreements.Partner> partners, int agreementId) =>
        partners
            .Select(p => ConvertToDatabaseModel(p, agreementId))
            .ToList();
    private PartnerInAgreement ConvertToDatabaseModel(Model.Agreements.Partner newPartner, int agreementId)
    {
        var partner = new PartnerInAgreement()
        {
            AgreementId = agreementId,
            PartnerInAgreementId = newPartner.Id,
            ContactPersons = newPartner.ContactPersons,
        };
        context.Attach(partner);
        return partner;
    }
    private static ModelAgreement ConvertToModel(Agreement agreement) => new()
    {

    };
}
