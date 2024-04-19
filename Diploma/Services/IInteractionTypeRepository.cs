using DataBase.Models;

namespace Diploma.Services;

public interface IInteractionTypeRepository
{
    public Task<List<InteractionType>> GetInteractionTypes();
    public Task<InteractionType> GetInteractionTypeById(int id);
    public Task DeleteInteractionTypeById(int id);
    public Task UpdateInteractionType(int id, InteractionType interactionType);
    public Task AddInteractionType(InteractionType interactionType);
}
