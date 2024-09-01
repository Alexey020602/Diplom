using Client.Services.Api.BaseApi;
using Model;

namespace Client.Services.Api;

public interface IDirectionsService: IReadApi<Direction>, IReadOneApi<Direction, int>, IDeleteApi<int>, IUpdateApi<Direction, int>, ICreateApi<Direction>
{
}
