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
    protected string? NamesFilter = null;

    private bool IsShowList => Items.Any();
    protected abstract string CreateHref { get; }
    protected abstract RenderFragment Filters { get; }

    protected override Task OnInitializedAsync()
    {
        return LoadItems(new ());
    }

    protected async Task LoadItems(LoadDataArgs args)
    {
        isLoading = true;
        Items = [];
        Items = await Load(NamesFilter, args.Skip, args.Top);
        isLoading = false;
    }
    protected abstract Task<List<TItem>> Load(string? text, int? skip, int? take);

    private void ClearNameFilter()
    {
        NamesFilter = string.Empty;
    }

    private Task ClearFilters()
    {
        ClearNameFilter();
        ClearFilterFields();
        return LoadItems(new ());
    }
    protected abstract void ClearFilterFields();
    protected abstract string RowHref(TItem item);
}