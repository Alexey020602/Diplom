using Client.Services.BaseApi;
using Model.Interactions;
using Refit;
using Interaction = DataBase.Models.Interaction;
namespace Client.Services;

public interface IInteractionsService: IReadOneApi<Interaction,int>, IUpdateApi<Interaction, int>, IDeleteApi<int>
{
    [Get("")] Task<List<InteractionShort>> ReadAll(int? interactionTypeId = null);
}
