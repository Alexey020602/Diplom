using Client.Services.Api.BaseApi;
using Model.Interactions;
using Refit;

namespace Client.Services.Api;

public interface IInteractionsService : IReadOneApi<Interaction, int>, IUpdateApi<Interaction, int>, IDeleteApi<int>,
    ICreateApi<Interaction>
{
    [Get("")]
    Task<List<InteractionShort>> ReadAll(string? code = null, 
        int? interactionTypeId = null, 
        DateOnly? sign = null, 
        DateOnly? start = null,
        DateOnly? end = null, 
        int? skip = null,
        int? take = null);
}