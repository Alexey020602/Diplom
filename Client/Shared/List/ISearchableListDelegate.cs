using Microsoft.AspNetCore.Components;

namespace Client.Shared.List;

public interface ISearchableListDelegate<TItem>
{
    public RenderFragment Filters { get; }
    public string PathForCreate { get; }
    public Task<List<TItem>> LoadItems();
    public string Row(TItem item);
}