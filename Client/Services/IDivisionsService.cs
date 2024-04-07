using Refit;
using SharedModel;
using DataBase.Models;

namespace Client.Services;

public interface IDivisionsService: IReadOneApi<Division, int>, IDeleteApi<int>, ICreateApi<Division>, IUpdateApi<Division, int>
{
    [Get("")]
    Task<List<DivisionShort>> ReadAll(int? facultyId);
}
