using Client.Services.Api.BaseApi;
using Model.Divisions;
using Refit;

namespace Client.Services.Api;

public interface IDivisionsService : IReadApi<DivisionShort>, IReadOneApi<Division, int>, ICanDeleteApi<int>, IDeleteApi<int>,
    ICreateApi<Division>, IUpdateApi<Division, int>
{
    [Get("")]
    Task<List<DivisionShort>> ReadAll(
        string? shortName = null, 
        string? fullName = null, 
        int? facultyId = null,
        int skip = 0,
        int take = 10);
}