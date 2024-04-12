using Refit;
using SharedModel;

namespace Client.Services;

public interface IAgreementService
{
    [Get("")]
    Task<List<ListItem>> ReadAll(int? agreementTypeId = null, int? agreementStatusId = null);
}
