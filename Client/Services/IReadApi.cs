using Refit;
namespace Client.Services;

public interface IReadApi<T> where T: class
{
    [Get("")] 
    Task<List<T>> ReadAll();
}



