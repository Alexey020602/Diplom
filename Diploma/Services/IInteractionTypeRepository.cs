// using DataBase.Models;

using Model.Interactions;

namespace Diploma.Services;

public interface IInteractionTypeRepository
{
    public Task<List<InteractionType>> GetInteractionTypes();
    public Task<InteractionType> GetInteractionTypeById(int id);
    public Task DeleteInteractionTypeById(int id);
    public Task UpdateInteractionType(int id, InteractionType interactionInteractionType);
    public Task AddInteractionType(InteractionType interactionInteractionType);
}
