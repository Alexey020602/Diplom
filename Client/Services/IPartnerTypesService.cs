using DataBase.Data;
using DataBase.Models;
using Refit;

namespace Client.Services;

public interface IPartnerTypesService
{
    [Get("/partnerTypes")]
    Task<IEnumerable<PartnerType>> GetPartnerTypesAsync();
    [Get("/partnerTypes/{id}")]
    Task<PartnerType> GetPartnerTypeAsync(int id);
    [Post("/partnerTypes")]
    Task UpdatePartnerType([Body] PartnerType partnerType);
    [Put("/partnerTypes")]
    Task AddPartnerType([Body] PartnerType partnerType);
    [Delete("/partnerTypes/{id}")]
    Task DeletePartnerType(int id);
}
