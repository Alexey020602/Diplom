using Client.Services.BaseApi;
using DataBase.Models;
using Refit;
using SharedModel;

namespace Client.Services;

public interface IAgreementService: IReadOneApi<Agreement, int>
{
    [Get("")]
    Task<List<AgreementShort>> ReadAll(int? agreementTypeId = null, int? agreementStatusId = null);
}
