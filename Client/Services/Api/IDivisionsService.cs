using Client.Services.Api.BaseApi;
using Model.Divisions;
using Refit;

namespace Client.Services.Api;

public interface IDivisionsService : IPagingReadApi<DivisionShort>, IReadOneApi<Division, int>, ICanDeleteApi<int>, IDeleteApi<int>,
    ICreateApi<Division>, IUpdateApi<Division, int>, ICountApi
{
}