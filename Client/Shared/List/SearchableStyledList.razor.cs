using Microsoft.AspNetCore.Components;

namespace Client.Shared.List;

public abstract partial class SearchableStyledList<TItem> : ComponentBase
{
    private const string EmptyListMessage = "Список подразделений пуст";
    private const string LoadMessage = "Загрузка...";

    protected bool isLoading;

    //[Parameter]
    //public ISearchableListDelegate<TItem> ListDelegate { get; set; } = null!;
    protected List<TItem> items = [];
    private string message = string.Empty;
    private string namesFilter = string.Empty;

    private IEnumerable<TItem> ShowedItems => from item in items
        where item.ToString()?.Contains(namesFilter) ?? false
        orderby item.ToString()
        select item;

    private bool IsShowList => ShowedItems.Any();
    protected abstract string CreateHref { get; }
    protected abstract RenderFragment Filters { get; }

    protected override Task OnInitializedAsync()
    {
        return LoadItems();
    }

    protected async Task LoadItems()
    {
        items = [];
        message = LoadMessage;
        items = await Load();
        message = EmptyListMessage;
    }

    protected abstract Task<List<TItem>> Load();
    protected abstract string RowHref(TItem item);
}