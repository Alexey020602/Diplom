using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Client.Shared.Form;

public abstract partial class BaseForm<TItem> where TItem : class
{
    [Inject] private IJSRuntime JsRuntime { get; set; } = null!;
    [Parameter] public TItem Item { get; set; } = null!;
    [Parameter] public EventCallback OnSubmit { get; set; }
    protected abstract RenderFragment Content { get; }

    private async Task OnValueSubmit()
    {
        await OnSubmit.InvokeAsync();
        await JsRuntime.InvokeVoidAsync("history.back");
    }
}