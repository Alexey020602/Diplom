using DataBase.Data;
using DataBase.Models;
using Diploma.Extensions;
using Diploma.Services;
using Microsoft.EntityFrameworkCore;

namespace Diploma.Repositories;

public class InteractionRepository(ApplicationContext context): IInteractionRepository
{
    public Task AddInteraction(Interaction interaction)
    {
        throw new NotImplementedException();
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
}
