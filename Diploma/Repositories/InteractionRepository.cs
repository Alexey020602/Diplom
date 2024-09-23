using DataBase.Data;
using DataBase.Extensions;
using Diploma.Services;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.Divisions;
using Model.Extensions;
using Model.Interactions;
using Model.Partners;
using Model.Mappers;
using Division = DataBase.Models.Division;
using Interaction = DataBase.Models.Interaction;
using InteractionType = DataBase.Models.InteractionType;
using ModelInteraction = Model.Interactions.Interaction;
using Partner = DataBase.Models.Partner;

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

    public Task<List<InteractionShort>> GetInteractions(int? interactionTypeId)
    {
        return context
            .Interactions
            .AsNoTracking()
            .Include(i => i.InteractionType)
            .FilterByType(interactionTypeId)
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




    private static ModelInteraction ConvertToModel(Interaction interaction)
    {
        return new ModelInteraction
        {
            Id = interaction.Id,
            Partner = interaction.Partner.ConvertToPartnerShort(),//ConvertToPartnerShort(interaction.Partner),
            Division = interaction.Division.ConvertToDivisionShort(),
            Type = interaction.InteractionType.ConvertToModel(),
            Theme = interaction.Theme,
            ContactCode = interaction.ContactCode,
            SigningDate = DateOnly.FromDateTime(interaction.SigningDateTime),
            Begin = DateOnly.FromDateTime(interaction.BeginigDateTime),
            End = DateOnly.FromDateTime(interaction.EndingDateTime),
            Directions = interaction.Directions.Select(d => d.ConvertToModel()).ToList()
        };
    }

    private static List<Direction> ConvertToModel(IEnumerable<DataBase.Models.Direction> directions)
    {
        return directions.Select(DirectionExtensions.ConvertToModel).ToList();
    }

    private static PartnerShort ConvertToPartnerShort(Partner partner)
    {
        return new PartnerShort(partner.Id, partner.ShortName);
    }

    private static DivisionShort ConvertToDivisionShort(Division division)
    {
        return new DivisionShort(division.Id, division.ShortName);
    }

    private static Model.Interactions.InteractionType ConvertToModel(InteractionType type)
    {
        return new Model.Interactions.InteractionType
        {
            Id = type.Id,
            Name = type.Name
        };
    }

    private void AttachDirections(IEnumerable<DataBase.Models.Direction> directions)
    {
        context.AttachRange(directions);
    }

    

    
}