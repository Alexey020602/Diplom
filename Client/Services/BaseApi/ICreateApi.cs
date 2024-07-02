using Refit;

namespace Client.Services.BaseApi;

public interface ICreateApi<in T> where T : class
{
    [Post("")]
    Task Create([Body] T payload);
}
