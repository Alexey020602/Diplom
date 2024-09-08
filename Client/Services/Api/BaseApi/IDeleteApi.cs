using Refit;

namespace Client.Services.Api.BaseApi;

public interface IDeleteApi<in Id>
{
    [Delete("/{id}")]
    Task Delete(Id id);
}