using Client.Services.BaseApi;
using Model.Agreements;
using Refit;

namespace Client.Services;

public interface IAgreementService: IReadOneApi<Agreement, int>, ICreateApi<Agreement>, IUpdateApi<Agreement, int>, IDeleteApi<int>
{
    [Get("")]
    Task<List<AgreementShort>> ReadAll(int? agreementTypeId = null, int? agreementStatusId = null);
}
