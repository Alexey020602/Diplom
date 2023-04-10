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
    Task<List<Partner>> GetPartners();
    
}

public class PartnersDataManager : IPartnersDataManager
{
    private ApplicationContext _context;
    public PartnersDataManager(ApplicationContext context)
    {
        _context = context;
    }
    
    public async Task<List<Partner>> GetPartners()
    {
        return await _context.Partners.ToListAsync();
    }

    public async Task<List<Partner>> GetPartner(string? shortName = null)
    {
        var partners =
            from p in _context.Partners
            where p.ShortName.ToLower().Contains(shortName.ToLower())
            select p;
        return await partners.ToListAsync();
    }

    public async Task AddPartnerAsync(Partner partner)
    {
        _context.Partners.Add(partner);
        await _context.SaveChangesAsync();
    }

    public void AddPartner(Partner partner)
    {
        _context.Partners.Add(partner);
        _context.SaveChanges();
    }

    public void DeletePartner(Partner partner)
    {
        _context.Partners.Remove(partner);
        _context.SaveChanges();
    }
    public async Task DeletePartnerAsync(Partner partner)
    {
        _context.Partners.Remove(partner);
        await _context.SaveChangesAsync();
    }
}