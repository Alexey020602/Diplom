using Model;
using Refit;
using Client.Services.BaseApi;
namespace Client.Services;

public interface IDirectionsService: IReadApi<Direction>, IReadOneApi<Direction, int>, IDeleteApi<int>, IUpdateApi<Direction, int>, ICreateApi<Direction>
{
}
