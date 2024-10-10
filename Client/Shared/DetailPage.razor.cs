using Microsoft.AspNetCore.Components;

namespace Client.Shared;

public abstract partial class DetailPage<TItem> : ComponentBase
{
    protected TItem? Item;
    [Parameter] public int Id { get; set; }
    protected abstract RenderFragment? Header(TItem item);
    protected abstract string? Title { get; }
    protected abstract string EntitiesPath { get; }
    protected abstract Task Delete();
    protected virtual bool CanDelete => true;
    protected virtual string CanNotDeleteMessage => "Нельзя удалить";
    private string EditPath => $"{EntitiesPath}/{Id}/edit";
    protected abstract Task<TItem> LoadItem();
    protected override async Task OnParametersSetAsync()
    {
        Item = await LoadItem();
    }
    protected abstract IEnumerable<Row> Rows(TItem item);
}