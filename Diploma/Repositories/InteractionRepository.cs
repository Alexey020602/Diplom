using DataBase.Data;
using DataBase.Extensions;
using Diploma.Services;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.Divisions;
using Model.Extensions;
using Model.Interactions;
using Model.Partners;
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
        var newInteraction = ConvertToDatabaseModel(interaction);
        AttachDirections(newInteraction.Directions);
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
        return ConvertToModel(await context.Interactions.AsNoTracking().FirstAsync(i => i.Id == id));
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
        var newInteraction = ConvertToDatabaseModel(interaction);
        var existingInteraction = await context.Interactions.Include(i => i.Directions).FirstAsync(i => i.Id == id);
        context.Entry(existingInteraction).CurrentValues.SetValues(interaction);
        existingInteraction.InteractionType = newInteraction.InteractionType;
        existingInteraction.Partner = newInteraction.Partner;
        existingInteraction.Division = newInteraction.Division;
        existingInteraction.Directions.UpdateByEnumerable(newInteraction.Directions);
        await context.SaveChangesAsync();
    }

    private Interaction ConvertToDatabaseModel(ModelInteraction interaction)
    {
        return new Interaction
        {
            Id = interaction.Id,
            InteractionType = ConvertToDatabaseModel(interaction.Type!),
            Theme = interaction.Theme,
            ContactCode = interaction.ContactCode,
            SigningDateTime = interaction.SigningDate.ToDateTime(default),
            BeginigDateTime = interaction.Begin.ToDateTime(default),
            EndingDateTime = interaction.End.ToDateTime(default),
            Division = GetDivisionFormDivisionShort(interaction.Division!),
            Partner = GetPartnerFromPartnerShort(interaction.Partner!),
            Directions = interaction.Directions.Select(DirectionExtensions.ConvertToDao).ToList()
        };
    }

    private InteractionType ConvertToDatabaseModel(Model.Interactions.InteractionType interactionType)
    {
        var newType = new InteractionType
        {
            Id = interactionType.Id,
            Name = interactionType.Name
        };
        context.Attach(newType);
        return newType;
    }

    private static ModelInteraction ConvertToModel(Interaction interaction)
    {
        return new ModelInteraction
        {
            Id = interaction.Id,
            Partner = ConvertToModel(interaction.Partner),
            Division = ConvertToModel(interaction.Division),
            Type = ConvertToModel(interaction.InteractionType),
            Theme = interaction.Theme,
            ContactCode = interaction.ContactCode,
            SigningDate = DateOnly.FromDateTime(interaction.SigningDateTime),
            Begin = DateOnly.FromDateTime(interaction.BeginigDateTime),
            End = DateOnly.FromDateTime(interaction.EndingDateTime),
            Directions = ConvertToModel(interaction.Directions)
        };
    }

    private static List<Direction> ConvertToModel(IEnumerable<DataBase.Models.Direction> directions)
    {
        return directions.Select(DirectionExtensions.ConvertToModel).ToList();
    }

    private static PartnerShort ConvertToModel(Partner partner)
    {
        return new PartnerShort(partner.Id, partner.ShortName);
    }

    private static DivisionShort ConvertToModel(Division division)
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

    private Partner GetPartnerFromPartnerShort(PartnerShort partner)
    {
        var exitingPartner = new Partner
        {
            Id = partner.Id
        }; //await context.Partners.FirstAsync(p => p.Id == partner.Id);
        context.Attach(exitingPartner);
        return exitingPartner;
    }

    private Division GetDivisionFormDivisionShort(DivisionShort division)
    {
        var existingDivision = new Division
        {
            Id = division.Id
        };
        context.Attach(existingDivision);
        return existingDivision;
    }
}