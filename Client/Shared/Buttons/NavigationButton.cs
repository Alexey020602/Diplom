using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen.Blazor;

namespace Client.Shared.Buttons;

public class NavigationButton: RadzenButton
{
    [Inject] private NavigationManager NavigationManager { get; set; } = default!;
    [Parameter] public string Path { get; set; } = default!;
    public override Task OnClick(MouseEventArgs args)
    {
        NavigationManager.NavigateTo(Path);
        return Task.CompletedTask;
    }
}