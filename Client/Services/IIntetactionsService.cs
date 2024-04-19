using Client.Services.BaseApi;
using DataBase.Models;
using Refit;
using SharedModel;

namespace Client.Services;

public interface IIntetactionsService: IReadOneApi<Interaction,int>, IUpdateApi<Interaction, int>, IDeleteApi<int>
{
    [Get("")] Task<List<InteractionShort>> ReadAll(int? interactionTypeId = null);
}
