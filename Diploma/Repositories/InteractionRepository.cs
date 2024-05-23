using DataBase.Data;
using DataBase.Models;
using Diploma.Extensions;
using Diploma.Services;
using Microsoft.EntityFrameworkCore;
using Model.Divisions;
using Model.Extensions;
using Model.Partners;
using Division = DataBase.Models.Division;
using Interaction = DataBase.Models.Interaction;
using Partner = DataBase.Models.Partner;

namespace Diploma.Repositories;

public class InteractionRepository(ApplicationContext context): IInteractionRepository
{
    public async Task AddInteraction(Model.Interactions.Interaction interaction)
    {
        var newInteraction = await ConvertToDatabaseModel(interaction);
        AttachDirections(newInteraction.Directions);
        context.Interactions.Add(newInteraction);
        await context.SaveChangesAsync();
    }

    public Task DeleteInteractionById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Interaction> GetInteractionById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Interaction>> GetInteractions(int? interactionTypeId) => context
        .Interactions.Include(i => i.InteractionType).FilterByType(interactionTypeId).ToListAsync();

    public Task UpdateInteraction(int id, Interaction interaction)
    {
        throw new NotImplementedException();
    }

    private async Task<Interaction> ConvertToDatabaseModel(Model.Interactions.Interaction interaction) => new()
    {
        Id = interaction.Id,
        InteractionType = new()
        {
            Id = interaction.Type!.Id,
            Name = interaction.Type!.Name
        },
        Theme = interaction.Theme,
        ContactCode = interaction.ContactCode,
        SigningDateTime = interaction.SigningDate,
        BeginigDateTime = interaction.Begin,
        EndingDateTime = interaction.End,
        Division = await GetDivisionFormDivisionShort(interaction.Division!),
        Partner = await GetPartnerFromPartnerShort(interaction.Partner!),
        Directions = interaction.Directions.Select(DirectionExtensions.ConvertToDao).ToList()
    };
    
    private void AttachDirections(List<Direction> directions)
    {
        context.AttachRange(directions);
    }
    private Task<Partner> GetPartnerFromPartnerShort(PartnerShort partner) =>
        context.Partners.FirstAsync(p => p.Id == partner.Id);
    private Task<Division> GetDivisionFormDivisionShort(DivisionShort division) =>
        context.Divisions.FirstAsync(d => d.Id == division.Id);
}
