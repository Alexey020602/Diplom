using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using Radzen.Blazor;

namespace Client.Shared.Buttons;

public class BackButton: RadzenButton
{
    [Inject] private IJSRuntime JsRuntime { get; set; } = default!;
    private ValueTask Back() => JsRuntime.InvokeVoidAsync("history.back");
    public override Task OnClick(MouseEventArgs args)
    {
        return Back().AsTask();
    }
}