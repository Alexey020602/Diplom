using Client.Services.BaseApi;
using DataBase.Models;
using Model.Agreements;
using Refit;
using Agreement = DataBase.Models.Agreement;

namespace Client.Services;

public interface IAgreementService: IReadOneApi<Agreement, int>
{
    [Get("")]
    Task<List<AgreementShort>> ReadAll(int? agreementTypeId = null, int? agreementStatusId = null);
}
