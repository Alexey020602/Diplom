using Refit;
using DataBase.Models;
using Client.Services.BaseApi;
using Model.Divisions;

namespace Client.Services;

public interface IDivisionsService: IReadOneApi<Division, int>, IDeleteApi<int>, ICreateApi<Division>, IUpdateApi<Division, int>
{
    [Get("")]
    Task<List<DivisionShort>> ReadAll(int? facultyId);
}
