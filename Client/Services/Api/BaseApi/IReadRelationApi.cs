using Refit;

namespace Client.Services.Api.BaseApi;

public interface IReadRelationApi<T, Id> where T : class where Id : struct
{
    [Get("/{id}/{name}")] Task<List<T>> ReadAllRelational(string name);
}
