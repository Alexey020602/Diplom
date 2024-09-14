using Microsoft.AspNetCore.Components;

namespace Client.Shared;

public abstract partial class DetailPage<TItem> : ComponentBase
{
    protected TItem? Item;
    [Parameter] public int Id { get; set; }
    protected abstract RenderFragment Content(TItem item);
    protected abstract string? Title { get; }
    protected abstract string EntitiesPath { get; }
    protected abstract Task Delete();
    protected virtual Task<bool> CanDelete() => Task.FromResult(true);
    private string EditPath => $"{EntitiesPath}/{Id}/edit";
    protected abstract Task<TItem> LoadItem();
    protected override async Task OnParametersSetAsync()
    {
        Item = await LoadItem();
    }
}