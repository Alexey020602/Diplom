using Client.Services.BaseApi;
using Model.Interactions;
using Refit;
namespace Client.Services;

public interface IInteractionsService: IReadOneApi<Interaction,int>, IUpdateApi<Interaction, int>, IDeleteApi<int>, ICreateApi<Interaction>
{
    [Get("")] Task<List<InteractionShort>> ReadAll(int? interactionTypeId = null);
}
