using Client.Components.Interactions;
using Model;
using Refit;

namespace Client.Services.Api.BaseApi;

public interface IReadApi<T> where T : class
{
    [Get("")]
    Task<IReadOnlyList<T>> ReadAll();
}

public interface IPagingReadApi<T> where T : class
{
    [Get("")]
    Task<Paging<T>> ReadAll<TFilter>(TFilter filter);
}