using Microsoft.EntityFrameworkCore;
using DataBase.Data;
using DataBase.Models;
using Diploma.Extensions;
using DataBase.SupportingClasses;
namespace Diploma.Services;

public class PartnersRepository(ApplicationContext context) : IPartnersRepository
{
    public async Task<IEnumerable<Partner>> GetPartnersAsync() =>
        await GetPartners()
        .ToListAsync();
    public async Task<IEnumerable<Partner>> GetPartnersAsync(int? partnerTypeId, int? directionId) => 
        await GetPartners(partnerTypeId, directionId)
        .ToListAsync();

    private IQueryable<Partner> GetPartners(int? partnerTypeId, int? directionId) => GetPartnersWithPartnerTypesAndDirections()
        .FilterByType(partnerTypeId)
        .FilterByDirection(directionId);
    private IQueryable<Partner> GetPartners() => context.Partners.AsNoTracking();
    private IQueryable<Partner> GetPartnersWithPartnerTypesAndDirections() => GetPartners()
        .Include(p => p.PartnerType)
        .Include(p => p.Directions);
    public async Task<Partner> GetPartnerByIdAsync(int id) => await GetPartnersWithPartnerTypesAndDirections()
            .FirstOrDefaultAsync(partner => partner.Id == id)
           ?? throw new KeyNotFoundException("Partner not found");


    public async Task AddPartnerAsync(Partner partner)
    {
        AttachDirections(partner.Directions);
        if (partner.PartnerType is not null) context.PartnerTypes.Attach(partner.PartnerType);
        context.Partners.Add(partner);
        Console.WriteLine(partner);
        await context.SaveChangesAsync();
    }

    private void AttachDirections(IEnumerable<Direction> directions)
    {
        context.Directions.AttachRange(directions);
    }

    public async Task<Partner> DeletePartnerByIdAsync(int id)
    {
        var partner = await context.Partners.FirstOrDefaultAsync(p => p.Id == id) ??
            throw new KeyNotFoundException("Partner not found");


        context.Partners.Remove(partner);
        await context.SaveChangesAsync();

        return partner;
    }

    public async Task UpdatePartnerAsync(Partner partner)
    {
        var existingPartner = await context.Partners
            .Include(p => p.Directions)
            .FirstAsync(p => p.Id == partner.Id) ??
            throw new KeyNotFoundException($"{partner.Id} не найден");

        context.Entry(existingPartner).CurrentValues.SetValues(partner);
        existingPartner.PartnerType = partner.PartnerType;
        existingPartner.Directions.UpdateByEnumerable(partner.Directions);
        await context.SaveChangesAsync();
    }
}
