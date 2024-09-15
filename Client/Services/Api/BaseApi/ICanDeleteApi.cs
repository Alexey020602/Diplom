using Refit;

namespace Client.Services.Api.BaseApi;

public interface ICanDeleteApi<in Id>
{
    [Get("/{id}/candelete")]
    Task<bool> CanDelete(Id id);
}