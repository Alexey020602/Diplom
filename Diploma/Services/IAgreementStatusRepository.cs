using DataBase.Models;

namespace Diploma.Services;

public interface IAgreementStatusRepository
{
    public Task<List<AgreementStatus>> GetAgreementStatuses();
    public Task<AgreementStatus> GetAgreementStatus(int id);
    public Task DeleteAgreementStatus(int id);
    public Task UpdateAgreementStatus(int id, AgreementStatus agreementStatus);
    public Task AddAgreementsStatus(AgreementStatus agreementStatus);
}
