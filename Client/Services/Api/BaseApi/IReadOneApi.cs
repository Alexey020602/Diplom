using Refit;

namespace Client.Services.Api.BaseApi;

public interface IReadOneApi<T, in Id> where T : class
{
    [Get("/{id}")]
    Task<T> ReadOne(Id id);
}