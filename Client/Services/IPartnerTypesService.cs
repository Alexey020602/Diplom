using DataBase.Data;
using DataBase.Models;
using Refit;

namespace Client.Services;

public interface IPartnerTypesService: IReadApi<PartnerType>
{
}
