using Microsoft.EntityFrameworkCore;
using DataBase.Data;
using DataBase.Models;

namespace Diploma.Services;

public class PartnersRepository: IPartnersRepository
{
    private ApplicationContext _context;
    public PartnersRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Partner>> GetPartnersAsync() => await _context.Partners.ToListAsync();
    

    public async Task<Partner> GetPartnerByIdAsync(int id)
    {
        var partner = await _context.Partners.FirstOrDefaultAsync(partner => partner.Id == id);

        if (partner == null)
        {
            throw new KeyNotFoundException("Partner not found");
        }

        return partner;
    }

    public async Task AddPartnerAsync(Partner partner)
    {
        if (partner.Directions is not null)
        {
            AttachDirections(partner.Directions);
        }
        _context.Partners.Add(partner);
        Console.WriteLine(partner);
        await _context.SaveChangesAsync();
    }

   private void AttachDirections(IEnumerable<Direction> directions)
    {
        foreach (var direction in directions)
        {
            _context.Directions.Attach(direction);
        }
    }

    public async Task<Partner> DeletePartnerByIdAsync(int id)
    {
        var partner = await _context.Partners.FirstOrDefaultAsync(p => p.Id == id);
        if (partner is  null)
        {
            throw new KeyNotFoundException("Partner not found");
        }

        _context.Partners.Remove(partner);
        await _context.SaveChangesAsync();

        return partner;
    }

    public async Task UpdatePartnerAsync(Partner partner)
    {
        _context.Partners.Update(partner);
        await _context.SaveChangesAsync();
    }
}
