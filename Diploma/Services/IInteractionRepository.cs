using ModelInteraction = Model.Interactions.Interaction;
using InteractionShort = Model.Interactions.InteractionShort;

namespace Diploma.Services;

public interface IInteractionRepository
{
    public Task<List<InteractionShort>> GetInteractions(int? interactionTypeId = null);
    public Task<ModelInteraction> GetInteractionById(int id);
    public Task DeleteInteractionById(int id);
    public Task UpdateInteraction(int id, ModelInteraction interaction);
    public Task AddInteraction(ModelInteraction interaction);
}