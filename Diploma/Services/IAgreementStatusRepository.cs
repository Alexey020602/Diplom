// using DataBase.Models;
using Model.Agreements;

namespace Diploma.Services;

public interface IAgreementStatusRepository
{
    public Task<List<Status>> GetAgreementStatuses();
    public Task<Status> GetAgreementStatus(int id);
    public Task DeleteAgreementStatus(int id);
    public Task UpdateAgreementStatus(int id, Status agreementStatus);
    public Task AddAgreementsStatus(Status agreementStatus);
}
