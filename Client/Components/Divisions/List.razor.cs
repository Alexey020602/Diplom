using Client.Services;
using Microsoft.AspNetCore.Components;
using Client.Shared.List;
using DataBase.Models;
using Model.Divisions;

namespace Client.Components.Divisions;

public partial class List: SearchableStyledList<DivisionShort>
{
    private Faculty? facultyFilterValue; 
    [Inject] private IDivisionsService DivisionsService { get; set; } = default!;
    protected override Task<List<DivisionShort>> Load() => DivisionsService.ReadAll(facultyFilterValue?.Id);
    protected override string RowHref(DivisionShort division) => $"divisions/{division.Id}";
    protected override string CreateHref => "divisions/create";
    private async Task SelectFaculty(Faculty? newFaculty)
    {
        facultyFilterValue = newFaculty;
        await LoadItems();
    }
}
