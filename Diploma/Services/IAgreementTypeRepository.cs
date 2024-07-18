// using DataBase.Models;

using Model.Agreements;

namespace Diploma.Services;

public interface IAgreementTypeRepository
{
    public Task<List<AgreementType>> GetAgreementTypes();
    public Task<AgreementType> GetAgreementType(int id);
    public Task DeleteAgreementType(int id);
    public Task UpdateAgreementType(int id, AgreementType agreementAgreementType);
    public Task AddAgreementType(AgreementType agreementAgreementType);
}
