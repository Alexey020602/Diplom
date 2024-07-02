using DataBase.Data;
using DataBase.Models;
using Diploma.Services;
using Microsoft.EntityFrameworkCore;

namespace Diploma.Repositories;

public class PartnerTypeRepository(ApplicationContext context) : IPartnerTypesRepository
{
    public async Task AddPartnerTypeAsync(PartnerType partnerType)
    {
        context.PartnerTypes.Add(partnerType);
        await context.SaveChangesAsync();
    }

    public async Task DeletePartnerTypeAsync(PartnerType partnerType)
    {
        context.PartnerTypes.Remove(partnerType);
        await context.SaveChangesAsync();
    }

    public async Task DeletePartnerTypeByIdAsync(int id)
    {
        var partnerType = context.PartnerTypes.FirstOrDefault(p => p.Id == id) ??
            throw new KeyNotFoundException("Не найден тип партнера");

        context.PartnerTypes.Remove(partnerType);
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<PartnerType>> GetAllPartnerTypesAsync() => await context.PartnerTypes.ToListAsync();


    public async Task<PartnerType> GetPartnerTypeByIdAsync(int id)
    {
        var partnerType = await context.PartnerTypes.FirstOrDefaultAsync(p => p.Id == id);

        return partnerType ?? throw new KeyNotFoundException("Не найден тип партнера");
    }


    public async Task UpdatePartnerTypeAsync(int id, PartnerType partnerType)
    {
        var existingPartnerType = await context.PartnerTypes.FindAsync(id) ?? 
            throw new KeyNotFoundException("Не найден тип партнера");
        context.Entry(existingPartnerType).CurrentValues.SetValues(partnerType);
        await context.SaveChangesAsync();
    }
}
