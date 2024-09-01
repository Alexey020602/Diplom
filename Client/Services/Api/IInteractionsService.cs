using Client.Services.Api.BaseApi;
using Model.Interactions;
using Refit;

namespace Client.Services.Api;

public interface IInteractionsService: IReadOneApi<Interaction,int>, IUpdateApi<Interaction, int>, IDeleteApi<int>, ICreateApi<Interaction>
{
    [Get("")] Task<List<InteractionShort>> ReadAll(int? interactionTypeId = null);
}
