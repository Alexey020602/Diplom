using ModelInteraction = Model.Interactions.Interaction;
using InteractionShort = Model.Interactions.InteractionShort;

namespace Diploma.Services;

public interface IInteractionRepository
{
    public Task<List<InteractionShort>> GetInteractions(
        string? code = null, 
        int? interactionTypeId = null,
        DateOnly? sign = null,
        DateOnly? begin = null, 
        DateOnly? end = null,
        int skip = 0,
        int take = 10);
    public Task<ModelInteraction> GetInteractionById(int id);
    public Task DeleteInteractionById(int id);
    public Task UpdateInteraction(int id, ModelInteraction interaction);
    public Task AddInteraction(ModelInteraction interaction);
}