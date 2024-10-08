using Microsoft.AspNetCore.Components;
using Radzen;

namespace Client.Shared.List;

public abstract partial class SearchableStyledList<TItem> : ComponentBase
{
    private const string EmptyListMessage = "Список подразделений пуст";
    private const string LoadMessage = "Загрузка...";   
    protected virtual string CreateText => "Добавить";
    protected string NameFilterTitle { get; set; } = "Введите полное название";
    private bool isLoading = false;
    //[Parameter]
    //public ISearchableListDelegate<TItem> ListDelegate { get; set; } = null!;
    protected List<TItem> Items = [];
    private string message = string.Empty;
    private string namesFilter = string.Empty;

    private IEnumerable<TItem> ShowedItems => from item in Items
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
        isLoading = true;
        Items = [];
        message = LoadMessage;
        Items = await Load();
        message = EmptyListMessage;
        isLoading = false;
    }
    protected abstract Task<List<TItem>> Load();

    private void ClearNameFilter()
    {
        namesFilter = string.Empty;
    }

    private Task ClearFilters()
    {
        ClearNameFilter();
        ClearFilterFields();
        return LoadItems();
    }
    protected abstract void ClearFilterFields();
    protected abstract string RowHref(TItem item);
}