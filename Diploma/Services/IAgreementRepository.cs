using Model;
using Model.Agreements;
using ModelAgreement = Model.Agreements.Agreement;

namespace Diploma.Services;

public interface IAgreementRepository
{
    public Task<Paging<AgreementShort>> GetAgreements(AgreementsFilter filter);
    public Task<ModelAgreement> GetAgreementById(int id);
    public Task DeleteAgreement(int id);
    public Task UpdateAgreement(int id, ModelAgreement agreement);
    public Task AddAgreement(ModelAgreement agreement);
    public Task<int> CountAgreements();
}