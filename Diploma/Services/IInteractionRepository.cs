using Model;
using Model.Interactions;
using ModelInteraction = Model.Interactions.Interaction;
using InteractionShort = Model.Interactions.InteractionShort;

namespace Diploma.Services;

public interface IInteractionRepository
{
    public Task<Paging<InteractionShort>> GetInteractions(InteractionsFilter filter);
    public Task<ModelInteraction> GetInteractionById(int id);
    public Task DeleteInteractionById(int id);
    public Task UpdateInteraction(int id, ModelInteraction interaction);
    public Task AddInteraction(ModelInteraction interaction);
    public Task<int> InteractionsCountAsync();
}