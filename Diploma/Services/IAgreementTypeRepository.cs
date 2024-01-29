using DataBase.Models;

namespace Diploma.Services;

public interface IAgreementTypeRepository
{
    public Task<IEnumerable<AgreementType>> GetAgreementTypes();
    public Task<AgreementType> GetAgreementType(int id);
    public Task DeleteAgreementType(int id);
    public Task UpdateAgreementType(AgreementType agreementType);
    public Task AddAgreementType(AgreementType agreementType);
}
