using DataBase.Data;
using DataBase.Extensions;
using Diploma.Services;
using Microsoft.EntityFrameworkCore;
using Model;
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

    public Task<int> InteractionsCountAsync() => context.Interactions.CountAsync();

    public async Task<Paging<InteractionShort>> GetInteractions(InteractionsFilter filter)
    {
        var sign = filter.Sign?.ToDateTime(new());
        var begin = filter.Start?.ToDateTime(new());
        var end = filter.End?.ToDateTime(new());
        var interactionsWithoutPaging = context
            .Interactions
            .AsNoTracking()
            .Include(i => i.InteractionType)
            .WhereWithNullable(sign, sign => (i => i.SigningDateTime.Date == sign))
            .WhereWithNullable(begin, begin => (i => i.SigningDateTime.Date == begin))
            .WhereWithNullable(end, end => (i => i.SigningDateTime.Date == end))
            .WhereWithNullable(filter.Code, code => (i => i.ContactCode.Contains(code)))
            .FilterByType(filter.InteractionTypeId)
            .OrderBy(i => i.Id);
        
        var take = filter.Take ?? 10;
        var skip = filter.Skip ?? 0;
        
        return new Paging<InteractionShort>(
            await interactionsWithoutPaging.CountAsync(),
            skip,
            take,
            await interactionsWithoutPaging
                .Skip(skip)
                .Take(take)
                .Select(i => new InteractionShort(i.Id, i.ToString()))
                .ToListAsync()
            );
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