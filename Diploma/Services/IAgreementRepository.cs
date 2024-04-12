using DataBase.Models;

namespace Diploma.Services;

public interface IAgreementRepository
{
    public Task<IEnumerable<Agreement>> GetAgreements(int? agreementTypeId, int? agreementStatusId);
    public Task<Agreement> GetAgreementById(int id);
    public Task DeleteAgreement(int id);
    public Task UpdateAgreement(int id, Agreement agreement);
    public Task AddAgreement(Agreement agreement);
}
