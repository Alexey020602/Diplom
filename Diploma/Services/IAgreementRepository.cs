using Model.Agreements;
using ModelAgreement = Model.Agreements.Agreement;

namespace Diploma.Services;

public interface IAgreementRepository
{
    public Task<List<AgreementShort>> GetAgreements(
        string? number = null,
        int? agreementTypeId = null,
        int? agreementStatusId = null, 
        DateOnly? startDate = null, 
        DateOnly? endDate = null,
        int skip = 0,
        int take = 10);
    public Task<ModelAgreement> GetAgreementById(int id);
    public Task DeleteAgreement(int id);
    public Task UpdateAgreement(int id, ModelAgreement agreement);
    public Task AddAgreement(ModelAgreement agreement);
}