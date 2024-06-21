using Client.Services;
using Client.Services.BaseApi;
using Model.Divisions;
using Division = Model.Agreements.Division;

namespace Client.Network;

public class DivisionsForAgreementService(IReadApi<DivisionShort> api): IDivisionsForAgreementService
{
    public async Task<List<Division>> GetDivisions() => (await api.ReadAll()).Select(ConvertFromShort).ToList();

    private Division ConvertFromShort(DivisionShort divisionShort) => new()
    {
        Id = divisionShort.Id,
        Description = divisionShort.Name,
    };
}