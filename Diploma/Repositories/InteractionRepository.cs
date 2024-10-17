using DataBase.Data;
using DataBase.Extensions;
using Diploma.Services;
using Microsoft.EntityFrameworkCore;
using Model.Interactions;
using Model.Mappers;
using Interaction = DataBase.Models.Interaction;
using ModelInteraction = Model.Interactions.Interaction;

namespace Diploma.Repositories;

public class InteractionRepository(ApplicationContext context) : IInteractionRepository
{
    public async Task AddInteraction(ModelInteraction interaction)
    {
        var newInteraction = interaction.ConvertToDatabaseModel();
        AttachEntities(newInteraction);
        await context.Interactions.AddAsync(newInteraction);
        await context.SaveChangesAsync();
    }

    public async Task DeleteInteractionById(int id)
    {
        context.Interactions.Remove(await context.Interactions.FirstAsync(i => i.Id == id));
        await context.SaveChangesAsync();
    }

    public async Task<ModelInteraction> GetInteractionById(int id)
    {
        return (await context.Interactions.AsNoTracking().FirstAsync(i => i.Id == id)).ConvertToModel();
    }

    public Task<List<InteractionShort>> GetInteractions(
        string? code = null, 
        int? interactionTypeId = null, 
        DateOnly? sign = null, 
        DateOnly? begin = null, 
        DateOnly? end = null,
        int skip = 0,
        int take = 10)
    {
        return context
            .Interactions
            .AsNoTracking()
            .Include(i => i.InteractionType)
            .FilterByDate(sign, i => i.SigningDateTime)
            .FilterByDate(begin, i => i.BeginigDateTime)
            .FilterByDate(end, i => i.EndingDateTime)
            .FilterByType(interactionTypeId)
            .OrderBy(i => i.Id)
            .Skip(skip)
            .Take(take)
            .Select(i => new InteractionShort(i.Id, i.ToString()))
            .ToListAsync();
    }

    public async Task UpdateInteraction(int id, ModelInteraction interaction)
    {
        var newInteraction = interaction.ConvertToDatabaseModel();
        AttachEntities(newInteraction);
        var existingInteraction = await context.Interactions
            .Include(i => i.Directions)
            .FirstAsync(i => i.Id == id);
        context.Entry(existingInteraction).CurrentValues.SetValues(interaction);
        
        existingInteraction.InteractionType = newInteraction.InteractionType;
        existingInteraction.Partner = newInteraction.Partner;
        existingInteraction.Division = newInteraction.Division;
        existingInteraction.Directions.UpdateByEnumerable(newInteraction.Directions);
        await context.SaveChangesAsync();
    }

    private void AttachEntities(Interaction interaction)
    {
        context.Partners.Attach(interaction.Partner);
        context.Divisions.Attach(interaction.Division);
        context.InteractionTypes.Attach(interaction.InteractionType);
        context.Directions.AttachRange(interaction.Directions);
    }
}