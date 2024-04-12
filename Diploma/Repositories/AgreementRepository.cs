namespace Diploma.Repositories;

using DataBase.Data;
using DataBase.Models;
using Diploma.Extensions;
using Microsoft.EntityFrameworkCore;
using Services;
using System.Collections.Generic;
using System.Threading.Tasks;

public class AgreementRepository(ApplicationContext context): IAgreementRepository
{
    public Task AddAgreement(Agreement agreement)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAgreement(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Agreement> GetAgreementById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Agreement>> GetAgreements(int? agreementTypeId, int? agreementStatusId) => 
        await GetAgreementWithTypeAndStatus()
        .FilterByType(agreementTypeId)
        .FilterBuStatus(agreementStatusId)
        .ToListAsync();
    private IQueryable<Agreement> GetAgreementWithTypeAndStatus() => context.Agreements
        .Include(x => x.AgreementType).
        Include(x => x.AgreementStatus);
    public Task UpdateAgreement(int id, Agreement agreement)
    {
        throw new NotImplementedException();
    }
}
