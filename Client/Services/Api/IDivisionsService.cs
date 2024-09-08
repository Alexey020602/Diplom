using Client.Services.Api.BaseApi;
using Model.Divisions;
using Refit;

namespace Client.Services.Api;

public interface IDivisionsService : IReadApi<DivisionShort>, IReadOneApi<Division, int>, IDeleteApi<int>,
    ICreateApi<Division>, IUpdateApi<Division, int>
{
    [Get("")]
    Task<List<DivisionShort>> ReadAll(int? facultyId);
}