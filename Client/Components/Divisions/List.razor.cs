using Client.Services;
using Microsoft.AspNetCore.Components;
using Client.Shared.List;
using SharedModel;
using DataBase.Models;
namespace Client.Components.Divisions;

public partial class List: SearchableStyledList<DivisionShort>
{
    private Faculty? faculty; 
    [Inject] private IDivisionsService DivisionsService { get; set; } = default!;
    protected override Task<List<DivisionShort>> Load() => DivisionsService.ReadAll(faculty?.Id);
    protected override string RowHref(DivisionShort division) => $"divisions/{division.Id}";
    protected override string CreateHref => "divisions/create";
    private async Task SelectFaculty(Faculty? faculty)
    {
        this.faculty = faculty;
        await LoadItems();
    }
}
