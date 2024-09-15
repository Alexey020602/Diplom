using Client.Services.Api;
using Client.Services.Api.BaseApi;
using Model.Agreements;
using Model.Divisions;

namespace Client.Network;

public class DivisionsForAgreementService(IReadApi<DivisionShort> api) : IDivisionsForAgreementService
{
    public async Task<List<DivisionInAgreement>> GetDivisions()
    {
        return (await api.ReadAll()).Select(ConvertFromShort).ToList();
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