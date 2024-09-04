using Blazored.LocalStorage;
using Client.Dto;
using Client.Services.Authorization;

namespace Client.Features;

public class AuthorizationStorage(ILocalStorageService localStorageService): ITokenStorage
{
    private const string StorageKey = "authorization";
    private readonly ILocalStorageService localStorageService = localStorageService;

    public async Task<Authorization?> GetAuthorization() =>
        await localStorageService.GetItemAsync<Authorization>(StorageKey);

    public async Task SetAuthorization(Authorization authorization) =>
        await localStorageService.SetItemAsync(StorageKey, authorization);

    public async Task RemoveAuthorization() => await localStorageService.RemoveItemAsync(StorageKey);
}