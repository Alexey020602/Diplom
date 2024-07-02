using Refit;

namespace Client.Services.BaseApi;

public interface IDeleteApi<in Id>
{
    [Delete("/{id}")]
    Task Delete(Id id);
}
