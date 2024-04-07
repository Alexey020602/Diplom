using Client.Services;
using Microsoft.AspNetCore.Components;
using Client.Shared.List;
using SharedModel;
namespace Client.Components.Divisions;

public partial class List: SearchableStyledList<DivisionShort>
{
    [Inject] private IDivisionsService DivisionsService { get; set; } = default!;
    protected override Task<List<DivisionShort>> Load() => DivisionsService.ReadAll(null);
    protected override string RowHref(DivisionShort division) => $"divisions/{division.Id}";
    protected override string CreateHref => "divisions/create";
}
