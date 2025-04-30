using Client.Services.Api;
using Client.Services.Api.BaseApi;
using Model.Agreements;
using Model.Divisions;

namespace Client.Network;

public class DivisionsForAgreementService(IPagingReadApi<DivisionShort> api) : IDivisionsForAgreementService
{
    public async Task<List<DivisionInAgreement>> GetDivisions()
    {
        var divisionShorts = await api.ReadAll(new DivisionsFilter());
        return divisionShorts.Data.Select(ConvertFromShort).ToList();
    }

    private DivisionInAgreement ConvertFromShort(DivisionShort divisionShort)
    {
        return new DivisionInAgreement
        {
            Id = divisionShort.Id,
            Description = divisionShort.Name
        };
    }
}