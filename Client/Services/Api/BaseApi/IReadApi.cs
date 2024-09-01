using Refit;

namespace Client.Services.Api.BaseApi;

public interface IReadApi<T> where T : class
{
    [Get("")]
    Task<List<T>> ReadAll();
}



