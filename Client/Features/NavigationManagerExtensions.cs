using Microsoft.AspNetCore.Components;

namespace Client.Features;

public static class NavigationManagerExtensions
{
    public static string GetCurrentPath(this NavigationManager navigationManager) => navigationManager.ToBaseRelativePath(navigationManager.Uri);
}