using DataBase.Data;
using DataBase.Models;
using Refit;

namespace Client.Services;

public interface IPartnerTypesService
{
    [Get("")]
    Task<IEnumerable<PartnerType>> GetPartnerTypesAsync();
    [Get("/{id}")]
    Task<PartnerType> GetPartnerTypeAsync(int id);
    [Post("")]
    Task UpdatePartnerType([Body] PartnerType partnerType);
    [Put("")]
    Task AddPartnerType([Body] PartnerType partnerType);
    [Delete("/{id}")]
    Task DeletePartnerType(int id);
}
