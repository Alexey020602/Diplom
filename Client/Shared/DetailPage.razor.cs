using Microsoft.AspNetCore.Components;

namespace Client.Shared;

public abstract partial class DetailPage<TItem> : ComponentBase
{
    protected TItem? item;
    [Parameter] public int Id { get; set; }
    protected abstract RenderFragment Content { get; }
    protected abstract string? Title { get; }
    protected abstract string EntitiesPath { get; }
    protected abstract bool CanDelete { get; }

    private string EditPath => $"{EntitiesPath}/{Id}/edit";
    protected abstract Task<TItem> LoadItem();

    protected override async Task OnParametersSetAsync()
    {
        item = await LoadItem();
    }
}