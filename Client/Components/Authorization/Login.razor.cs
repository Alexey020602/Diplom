using Client.Services.Authorization;
using Microsoft.AspNetCore.Components;
using Model.Identity;

namespace Client.Components.Authorization;

public partial class Login
{
    private AuthRequest AuthRequest { get; } = new();
    [Inject] private IAuthenticationService AuthenticationService { get; set; } = default!;
    [Inject] private NavigationManager NavigationManager { get; set; } = default!;
    private bool ShowAuthError { get; set; }
    private string Error { get; set; } = default!;

    private async Task ExecuteLogin()
    {
        ShowAuthError = false;
        await AuthenticationService.Login(AuthRequest);
        NavigationManager.NavigateTo("/");
    }
}