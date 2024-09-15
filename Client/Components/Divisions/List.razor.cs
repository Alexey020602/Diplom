using Client.Services.Api;
using Client.Shared.List;
using Microsoft.AspNetCore.Components;
using Model.Divisions;

namespace Client.Components.Divisions;

public partial class List : SearchableStyledList<DivisionShort>
{
    private Faculty? facultyFilterValue;
    [Inject] private IDivisionsService DivisionsService { get; set; } = default!;
    protected override string CreateHref => "divisions/create";

    protected override Task<List<DivisionShort>> Load()
    {
        return DivisionsService.ReadAll(facultyFilterValue?.Id);
    }

    protected override string RowHref(DivisionShort division)
    {
        return $"divisions/{division.Id}";
    }

    private async Task SelectFaculty(Faculty? newFaculty)
    {
        facultyFilterValue = newFaculty;
        await LoadItems();
    }
}