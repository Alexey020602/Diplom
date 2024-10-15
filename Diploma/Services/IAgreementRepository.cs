using Model.Agreements;
using ModelAgreement = Model.Agreements.Agreement;

namespace Diploma.Services;

public interface IAgreementRepository
{
    public Task<List<AgreementShort>> GetAgreements(
        string? number,
        int? agreementTypeId,
        int? agreementStatusId, 
        DateOnly? startDate = null, 
        DateOnly? endDate = null);
    public Task<ModelAgreement> GetAgreementById(int id);
    public Task DeleteAgreement(int id);
    public Task UpdateAgreement(int id, ModelAgreement agreement);
    public Task AddAgreement(ModelAgreement agreement);
}