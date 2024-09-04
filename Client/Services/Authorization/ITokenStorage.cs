namespace Client.Services.Authorization;

public interface ITokenStorage
{
    Task<Dto.Authorization?> GetAuthorization();
    Task SetAuthorization(Dto.Authorization authorization);
    Task RemoveAuthorization();
}