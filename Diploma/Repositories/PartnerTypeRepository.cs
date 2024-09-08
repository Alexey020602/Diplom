using DataBase.Data;
using Diploma.Services;
using Microsoft.EntityFrameworkCore;
using Model.Extensions;
using Type = Model.Partners.PartnerType;

namespace Diploma.Repositories;

public class PartnerTypeRepository(ApplicationContext context) : IPartnerTypesRepository
{
    public async Task AddPartnerTypeAsync(Type type)
    {
        var partnerType = type.ConvertToDao();
        context.PartnerTypes.Add(partnerType);
        await context.SaveChangesAsync();
    }

    public async Task DeletePartnerTypeAsync(Type type)
    {
        var partnerType = type.ConvertToDao();
        context.PartnerTypes.Remove(partnerType);
        await context.SaveChangesAsync();
    }

    public async Task DeletePartnerTypeByIdAsync(int id)
    {
        var partnerType = await context.PartnerTypes.FirstAsync(p => p.Id == id);
        context.PartnerTypes.Remove(partnerType);
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Type>> GetAllPartnerTypesAsync()
    {
        return await context.PartnerTypes.Select(t => t.ConvertToModel()).ToListAsync();
    }


    public async Task<Type> GetPartnerTypeByIdAsync(int id)
    {
        var partnerType = await context.PartnerTypes.FirstOrDefaultAsync(p => p.Id == id);

        return partnerType?.ConvertToModel() ?? throw new KeyNotFoundException("Не найден тип партнера");
    }


    public async Task UpdatePartnerTypeAsync(int id, Type partnerPartnerType)
    {
        var existingPartnerType = await context.PartnerTypes.FindAsync(id) ??
                                  throw new KeyNotFoundException("Не найден тип партнера");
        context.Entry(existingPartnerType).CurrentValues.SetValues(partnerPartnerType.ConvertToDao());
        await context.SaveChangesAsync();
    }
}