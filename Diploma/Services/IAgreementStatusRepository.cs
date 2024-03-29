﻿using DataBase.Models;

namespace Diploma.Services;

public interface IAgreementStatusRepository
{
    public Task<IEnumerable<AgreementStatus>> GetAgreementStatuses();
    public Task<AgreementStatus> GetAgreementStatus(int id);
    public Task DeleteAgreementStatus(int id);
    public Task UpdateAgreementStatus(AgreementStatus agreementStatus);
    public Task AddAgreementsStatus(AgreementStatus agreementStatus);
}
