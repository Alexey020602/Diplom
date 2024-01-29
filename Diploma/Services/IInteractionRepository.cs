using DataBase.Models;

namespace Diploma.Services;

public interface IInteractionRepository
{
    public Task<IEnumerable<Interaction>> GetInteractions();
    public Task<Interaction> GetInteractionById(int id);
    public Task DeleteInteractionById(int id);
    public Task UpdateInteraction(Interaction interaction);
    public Task AddInteraction(Interaction interaction);
}
