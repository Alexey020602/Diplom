using Microsoft.EntityFrameworkCore;
using DataBase.Data;
using DataBase.Models;

namespace Diploma.Services;

public class PartnersRepository(ApplicationContext context) : IPartnersRepository
{
    public async Task<IEnumerable<Partner>> GetPartnersAsync() =>
        await context.Partners
        .Include(p => p.PartnerType)
        .ToListAsync();


    public async Task<Partner> GetPartnerByIdAsync(int id)
    {
        var partner = await context.Partners
            .Include(p=> p.PartnerType)
            .Include(p=> p.Directions)
            .FirstOrDefaultAsync(partner => partner.Id == id) 
           ?? throw new KeyNotFoundException("Partner not found");
        

        return partner;
    }

    public async Task AddPartnerAsync(Partner partner)
    {
        if (partner.Directions is not null)
        {
            AttachDirections(partner.Directions);
        }
        context.Partners.Add(partner);
        Console.WriteLine(partner);
        await context.SaveChangesAsync();
    }

    private void AttachDirections(IEnumerable<Direction> directions)
    {
        foreach (var direction in directions)
        {
            context.Directions.Attach(direction);
        }
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
        context.Partners.Update(partner);
        await context.SaveChangesAsync();
    }
}
