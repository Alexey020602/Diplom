using DataBase.Models;

namespace Diploma.Services;

public interface IInteractionTypeRepository
{
    public Task<IEnumerable<InteractionType>> GetInteractionTypes();
    public Task<InteractionType> GetInteractionTypeById(int id);
    public Task DeleteInteractionTypeById(int id);
    public Task UpdateInteractionType(InteractionType interactionType);
    public Task AddInteractionType(InteractionType interactionType);
}
