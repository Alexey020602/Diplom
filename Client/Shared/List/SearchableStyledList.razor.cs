using Microsoft.AspNetCore.Components;
using Model;
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
    protected IReadOnlyList<TItem> Items = [];
    protected string? NamesFilter = null;
    protected int pageSize = 5;
    protected int count = 0;
    private int page = 1;
    protected abstract string CreateHref { get; }
    protected abstract RenderFragment Filters { get; }

    protected override Task OnInitializedAsync()
    {
        return LoadItems(new LoadDataArgs
        {
            Top = pageSize,
        }) ;
    }

    private Task PageChanged(PagerEventArgs args)
    {
        page = args.PageIndex;
        return Task.CompletedTask;
    }
     
    protected async Task LoadItems(LoadDataArgs args)
    {
        isLoading = true;
        // Items.AddRange(await Load(NamesFilter, args.Skip, args.Top));
        Items = [];
        var paging = await Load(NamesFilter, args.Skip, args.Top);
        count = paging.Count;
        Items = paging.Data;
        isLoading = false;
    }
    protected abstract Task<Paging<TItem>> Load(string? text, int? skip, int? take);
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