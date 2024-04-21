using Microsoft.AspNetCore.Components;

namespace Client.Shared;

public abstract partial class DetailPage<TItem>: ComponentBase
{
    [Parameter] public int Id { get; set; }
    protected abstract RenderFragment Content { get; }
    protected abstract string? Title { get; }
    protected abstract string EntitiesPath { get; }
    private string EditPath => $"{EntitiesPath}/{Id}/edit";
}