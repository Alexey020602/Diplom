using Microsoft.EntityFrameworkCore;

namespace Diploma;
using DataBase.Models;
using DataBase.Data;
using System.Linq;

public interface IPartnersDataManager
{
    Task AddPartnerAsync(Partner partner);
    void AddPartner(Partner partner);
    void DeletePartner(Partner partner);
    Task DeletePartnerAsync(Partner partner);
    Task DeletePartnerAsync(int id);
    Task<Partner?> EditPartner(int id);
    Task EditPartner(Partner partner);
    Task<List<Partner>> GetPartnersAsync();
    Task<List<Partner>> GetPartnerAsync(string? shortName = null /*, PartnerType? partnerType = null*/);
}

public class PartnersDataManager : IPartnersDataManager
{
    private ApplicationContext _context;
    public PartnersDataManager(ApplicationContext context)
    {
        _context = context;
    }
    
    public async Task<List<Partner>> GetPartnersAsync()
    {
        return await _context.Partners.ToListAsync();
    }

    public async Task<List<Partner>> GetPartnerAsync(string? shortName = null/*, PartnerType? partnerType = null*/)
    {
        var partners =
            from p in _context.Partners
            where p.ShortName.ToLower().Contains(shortName.ToLower())/* && p.PartnerType == partnerType*/
            select p;
        return await partners.ToListAsync();
    }

    public async Task AddPartnerAsync(Partner partner)
    {
        var types = 
            from p in _context.PartnerTypes 
            where p.Id == 1 
            select p;
        partner.PartnerType = types.Single();
        _context.Partners.Add(partner);
        Console.WriteLine(partner);
        await _context.SaveChangesAsync();
    }

    public void AddPartner(Partner partner)
    {
        var types = 
            from p in _context.PartnerTypes 
            where p.Id == 1 
            select p;
        partner.PartnerType = types.Single();
        Console.WriteLine(partner);
        _context.Partners.Add(partner);
        _context.SaveChanges();
    }

    public async Task<Partner?> EditPartner(int id)
    {
        return await _context.Partners.FirstOrDefaultAsync(p => p.Id == id);
    }
    public void DeletePartner(Partner partner)
    {
        _context.Partners.Remove(partner);
        _context.SaveChanges();
    }
    public async Task DeletePartnerAsync(Partner partner)
    {
        //_context.Entry(partner).State = EntityState.Deleted;
        _context.Partners.Remove(partner);
        await _context.SaveChangesAsync();
    }

    public async Task DeletePartnerAsync(int id)
    {
        await this.DeletePartnerAsync(new Partner { Id = id });
    }

    public async Task EditPartner(Partner partner)
    {
        _context.Partners.Update(partner);
        await _context.SaveChangesAsync();
    }
}