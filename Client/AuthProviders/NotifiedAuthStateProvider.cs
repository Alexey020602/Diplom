using Client.Dto;
using Microsoft.AspNetCore.Components.Authorization;

namespace Client.AuthProviders;

public abstract class NotifiedAuthStateProvider : AuthenticationStateProvider
{
    public abstract Task Logout();
    public abstract Task Login(Authorization authorization);
}