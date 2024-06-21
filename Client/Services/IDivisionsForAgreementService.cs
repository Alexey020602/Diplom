using Model.Agreements;

namespace Client.Services;

public interface IDivisionsForAgreementService
{
    Task<List<Division>> GetDivisions();
}