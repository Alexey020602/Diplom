using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
namespace Client.Shared.Form;

public abstract partial class BaseForm<TItem> where TItem : class
{
    [Inject] private IJSRuntime JSRuntime { get; set; } = null!;
    [Parameter] public TItem Item { get; set; } = null!;
    [Parameter] public EventCallback OnSubmit { get; set; }
    protected async Task OnValueSubmit()
    {
        await OnSubmit.InvokeAsync();
        await JSRuntime.InvokeVoidAsync("history.back");
    }
    protected abstract RenderFragment Content { get; }
}
