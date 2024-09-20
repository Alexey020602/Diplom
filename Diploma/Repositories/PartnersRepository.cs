using DataBase.Data;
using DataBase.Extensions;
using DataBase.Models;
using Diploma.Mappers;
using Diploma.Services;
using Microsoft.EntityFrameworkCore;
using Model.Extensions;
using Model.Partners;
using DBPartner = DataBase.Models.Partner;
using Partner = Model.Partners.Partner;

namespace Diploma.Repositories;

public class PartnersRepository(ApplicationContext context) : IPartnersRepository
{
    public async Task<List<AgreementInPartner>> GetAgreementsForPartnerWithId(int id)
    {
        return (
                await GetPartnersWithAgreements()
                    .FirstAsync(p => p.Id == id)
            )
            .PartnersInAgreement
            .Select(p => p.Agreement.ConvertToPartnerModel())
            .ToList();
    }

    public async Task<List<InteractionInPartner>> GetInteractionsForPartnerWithId(int id)
    {
        return (
                await GetPartnersWithInteractions()
                    .Include(p => p.Interactions)
                    .FirstAsync(p => p.Id == id)
            )
            .Interactions
            .Select(i => i.ConvertToPartnerModel())
            .ToList();
    }

    public async Task<IEnumerable<Partner>> GetPartnersAsync(int? partnerTypeId, int? directionId)
    {
        return await GetPartners(partnerTypeId, directionId)
            .Select(p => p.ConvertToModel())
            .ToListAsync();
    }

    public async Task<Partner> GetPartnerByIdAsync(int id)
    {
        return (await GetPartnersWithTypesAndDirections()
            .Include(p => p.Interactions)
            .Include(P => P.PartnersInAgreement)
            .ThenInclude(p => p.Agreement)
            // .Select(p => p.ConvertToModel())
            .FirstAsync(partner => partner.Id == id)).ConvertToModel();
    }

    public async Task<bool> CanDeletePartner(int id)
    {
        var partner = await context.Partners.AsNoTracking().Include(p => p.PartnersInAgreement)
            .Include(p => p.Interactions).FirstAsync(p => p.Id == id);

        return partner.PartnersInAgreement.Count == 0 && partner.Interactions.Count == 0;
    }

    public async Task AddPartnerAsync(Partner newPartner)
    {
        var partner = newPartner.ConvertToDao();
        AttachDirections(partner.Directions);
        context.PartnerTypes.Attach(partner.PartnerType);
        context.Partners.Add(partner);
        Console.WriteLine(partner);
        await context.SaveChangesAsync();
    }

    public async Task<Partner> DeletePartnerByIdAsync(int id)
    {
        var partner = await context.Partners.FirstOrDefaultAsync(p => p.Id == id) ??
                      throw new KeyNotFoundException("Partner not found");

        context.Partners.Remove(partner);
        await context.SaveChangesAsync();
        return partner.ConvertToModel();
    }

    public async Task UpdatePartnerAsync(int id, Partner newPartner)
    {
        var partner = newPartner.ConvertToDao();
        var existingPartner = await context.Partners
                                  .Include(p => p.Directions)
                                  .FirstAsync(p => p.Id == id) ??
                              throw new KeyNotFoundException($"{partner.Id} не найден");

        context.Entry(existingPartner).CurrentValues.SetValues(partner);
        existingPartner.PartnerType = partner.PartnerType;
        existingPartner.Directions.UpdateByEnumerable(partner.Directions);
        await context.SaveChangesAsync();
    }

    private IQueryable<DBPartner> GetPartnersWithAgreements()
    {
        return context.Partners
            .Include(p => p.PartnersInAgreement)
            .ThenInclude(partnerInAgreement => partnerInAgreement.Agreement)
            .ThenInclude(agreement => agreement.AgreementType)
            .Include(p => p.PartnersInAgreement)
            .ThenInclude(partnerInAgreement => partnerInAgreement.Agreement)
            .ThenInclude(agreement => agreement.AgreementStatus);
    }

    private IQueryable<DBPartner> GetPartnersWithInteractions()
    {
        return context.Partners
            .Include(p => p.Interactions)
            .ThenInclude(i => i.InteractionType);
    }

    private void AttachDirections(IEnumerable<Direction> directions)
    {
        context.Directions.AttachRange(directions);
    }

    private IQueryable<DBPartner> GetPartners(int? partnerTypeId, int? directionId)
    {
        return GetPartnersWithTypesAndDirections()
            .FilterByType(partnerTypeId)
            .FilterByDirection(directionId);
    }

    private IQueryable<DBPartner> GetPartners()
    {
        return context.Partners.AsNoTracking();
    }

    private IQueryable<DBPartner> GetPartnersWithTypesAndDirections()
    {
        return GetPartners()
            .Include(p => p.PartnerType)
            .Include(p => p.Directions);
    }
}