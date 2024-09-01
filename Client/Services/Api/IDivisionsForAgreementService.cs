using Model.Agreements;

namespace Client.Services.Api;

public interface IDivisionsForAgreementService
{
    Task<List<DivisionInAgreement>> GetDivisions();
}