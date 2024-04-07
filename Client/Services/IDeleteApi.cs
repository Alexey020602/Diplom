using Refit;

namespace Client.Services;

public interface IDeleteApi<in Id>
{
    [Delete("/{id}")]
    Task Delete(Id id);
}
