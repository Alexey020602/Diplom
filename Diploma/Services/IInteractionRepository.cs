using DataBase.Models;

namespace Diploma.Services;

public interface IInteractionRepository
{
    public Task<List<Interaction>> GetInteractions(int? interactionTypeId = null);
    public Task<Interaction> GetInteractionById(int id);
    public Task DeleteInteractionById(int id);
    public Task UpdateInteraction(int id, Interaction interaction);
    public Task AddInteraction(Model.Interactions.Interaction interaction);
}
