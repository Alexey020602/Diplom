using DataBase.Data;
using DataBase.Models;
using Diploma.Services;
using Microsoft.EntityFrameworkCore;

namespace Diploma.Repositories;

public class InteractionTypeRepository(ApplicationContext context) : IInteractionTypeRepository
{
    public async Task AddInteractionType(InteractionType interactionType)
    {
        await context.InteractionTypes.AddAsync(interactionType);
        await context.SaveChangesAsync();
    }

    public async Task DeleteInteractionTypeById(int id)
    {
        var existingInteractionType = await context.InteractionTypes.SingleAsync(type => type.Id == id);
        context.InteractionTypes.Remove(existingInteractionType);
        await context.SaveChangesAsync();
    }

    public Task<InteractionType> GetInteractionTypeById(int id) => context.InteractionTypes.SingleAsync(type => type.Id == id);

    public Task<List<InteractionType>> GetInteractionTypes() => context.InteractionTypes.ToListAsync();

    public async Task UpdateInteractionType(int id, InteractionType interactionType)
    {
        var exitingInteractionType = await context.InteractionTypes.SingleAsync(type => type.Id == id);
        context.Entry(exitingInteractionType).CurrentValues.SetValues(interactionType);
        await context.SaveChangesAsync();
    }
}
