using Microsoft.EntityFrameworkCore;
using DataBase.Data;
using DataBase.Extensions;
using DataBase.Models;
using Diploma.Extensions;
using DataBase.SupportingClasses;
using Diploma.Services;

namespace Diploma.Repositories;

public class PartnersRepository(ApplicationContext context) : IPartnersRepository
{
    public async Task<List<Agreement>> GetAgreementsForPartnerWithId(int id) => 
        (
            await GetPartnersWithAgreements()
            .FirstAsync(p => p.Id == id)
        )
        .PartnersInAgreement
        .Select(p => p.Agreement)
        .ToList();
    public async Task<List<Interaction>> GetInteractionsForPartnerWithId(int id) => 
        (
            await GetPartnersWithInteractions()
            .FirstAsync(p => p.Id == id)
        )
        .Interactions
        .ToList();
    public async Task<IEnumerable<Partner>> GetPartnersAsync(int? partnerTypeId, int? directionId) =>
        await GetPartners(partnerTypeId, directionId)
            .ToListAsync();
    public async Task<Partner> GetPartnerByIdAsync(int id) => await GetPartnersWithTypesAndDirections()
        .FirstAsync(partner => partner.Id == id);
    public async Task AddPartnerAsync(Partner partner)
    {
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
        return partner;
    }
    public async Task UpdatePartnerAsync(int id, Partner partner)
    {
        var existingPartner = await context.Partners
                                  .Include(p => p.Directions)
                                  .FirstAsync(p => p.Id == id) ??
                              throw new KeyNotFoundException($"{partner.Id} не найден");

        context.Entry(existingPartner).CurrentValues.SetValues(partner);
        existingPartner.PartnerType = partner.PartnerType;
        existingPartner.Directions.UpdateByEnumerable(partner.Directions);
        await context.SaveChangesAsync();
    }

    private IQueryable<Partner> GetPartnersWithAgreements() => context.Partners
        .Include(p => p.PartnersInAgreement)
        .ThenInclude(partnerInAgreement => partnerInAgreement.Agreement);
    private IQueryable<Partner> GetPartnersWithInteractions() => context.Partners
        .Include(p => p.Interactions);
    private void AttachDirections(IEnumerable<Direction> directions)
    {
        context.Directions.AttachRange(directions);
    }
    private IQueryable<Partner> GetPartners(int? partnerTypeId, int? directionId) =>
        GetPartnersWithTypesAndDirections()
            .FilterByType(partnerTypeId)
            .FilterByDirection(directionId);
    private IQueryable<Partner> GetPartners() => context.Partners.AsNoTracking();
    private IQueryable<Partner> GetPartnersWithTypesAndDirections() => GetPartners()
        .Include(p => p.PartnerType)
        .Include(p => p.Directions);
}