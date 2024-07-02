using Microsoft.AspNetCore.Components;

namespace Client.Shared.List;

public abstract partial class SearchableStyledList<TItem>: ComponentBase
{
    //[Parameter]
    //public ISearchableListDelegate<TItem> ListDelegate { get; set; } = null!;
    protected List<TItem> items = [];
    private IEnumerable<TItem> ShowedItems => from item in items
                                              where item.ToString()?.Contains(namesFilter) ?? false
                                              orderby item.ToString()
                                              select item;
    private string namesFilter = string.Empty;
    private string message = string.Empty;
    private const string EmptyListMessage = "Список подразделений пуст";
    private const string LoadMessage = "Загрузка...";
    private bool IsShowList => ShowedItems.Any();

    protected override Task OnInitializedAsync() => LoadItems();

    protected async Task LoadItems()
    {
        items = [];
        message = LoadMessage;
        try
        {
            items = await Load();
            message = EmptyListMessage;
        }
        catch (Exception ex)
        {
            message = ex.Message;
        }
    }
    protected abstract Task<List<TItem>> Load();
    protected abstract string RowHref(TItem item);
    protected abstract string CreateHref {  get; }
    protected abstract RenderFragment Filters { get; }
}
