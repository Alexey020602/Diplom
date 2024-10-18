using Refit;

namespace Client.Services.Api.BaseApi;

public interface ICountApi
{
    [Get("/count")]
    public Task<int> GetCount();
}