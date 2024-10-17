using Client.Services.Api.BaseApi;
using Model.Agreements;
using Refit;

namespace Client.Services.Api;

public interface IAgreementService : IReadOneApi<Agreement, int>, ICreateApi<Agreement>, IUpdateApi<Agreement, int>,
    IDeleteApi<int>
{
    [Get("")]
    Task<List<AgreementShort>> ReadAll(
        string? number = null, 
        int? agreementTypeId = null, 
        int? agreementStatusId = null,
        DateOnly? startDate = null,
        DateOnly? endDate = null,
        int skip = 0,
        int take = 10);
}