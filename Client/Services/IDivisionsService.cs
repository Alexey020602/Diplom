using Refit;
using Client.Services.BaseApi;
using Model.Divisions;

namespace Client.Services;

public interface IDivisionsService: IReadApi<DivisionShort>, IReadOneApi<Division, int>, IDeleteApi<int>, ICreateApi<Division>, IUpdateApi<Division, int>
{
    [Get("")]
    Task<List<DivisionShort>> ReadAll(int? facultyId);
}
