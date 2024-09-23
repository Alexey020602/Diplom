using DataBase.Data;
using Diploma.Services;
using Microsoft.EntityFrameworkCore;
using Model.Extensions;
using Model.Mappers;
using Type = Model.Interactions.InteractionType;

namespace Diploma.Repositories;

public class InteractionTypeRepository(ApplicationContext context) : IInteractionTypeRepository
{
    public async Task AddInteractionType(Type type)
    {
        var interactionType = type.ConvertToDao();
        await context.InteractionTypes.AddAsync(interactionType);
        await context.SaveChangesAsync();
    }

    public async Task DeleteInteractionTypeById(int id)
    {
        var existingInteractionType = await context.InteractionTypes.SingleAsync(type => type.Id == id);
        context.InteractionTypes.Remove(existingInteractionType);
        await context.SaveChangesAsync();
    }

    public Task<Type> GetInteractionTypeById(int id)
    {
        return context
            .InteractionTypes
            .Select(t => t.ConvertToModel())
            .SingleAsync(type => type.Id == id);
    }

    public Task<List<Type>> GetInteractionTypes()
    {
        return context.InteractionTypes
            .Select(t => t.ConvertToModel())
            .ToListAsync();
    }

    public async Task UpdateInteractionType(int id, Type interactionInteractionType)
    {
        var exitingInteractionType = await context.InteractionTypes.SingleAsync(type => type.Id == id);
        context.Entry(exitingInteractionType).CurrentValues.SetValues(interactionInteractionType);
        await context.SaveChangesAsync();
    }
}