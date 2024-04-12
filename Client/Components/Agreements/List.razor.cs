using Client.Services;
using Microsoft.AspNetCore.Components;
using SharedModel;

namespace Client.Components.Agreements;

public partial class List
{
    [Inject] private IAgreementService AgreementService { get; set; } = null!;
    protected override string CreateHref => "/agreements/create";
    protected override Task<List<ListItem>> Load() => AgreementService.ReadAll(); 

    protected override string RowHref(ListItem item) => $"/agreements/{item.Id}";
}
