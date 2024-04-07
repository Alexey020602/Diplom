using Client.Services;
using DataBase.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using SharedModel;
namespace Client.Components.Divisions;

public partial class List
{
    [Inject] private IDivisionsService DivisionsService { get; set; } = default!;
    private IEnumerable<DivisionShort>? Divisions { get; set; }
    private IEnumerable<DivisionShort> ShowedDivisions => from division in Divisions ?? []
                                                     where division.Name.Contains(NameFilter)
                                                     select division;
    private string NameFilter { get; set; } = string.Empty;
    private string Message { get; set; } = null!;
    private const string EmptyListMessage = "Список подразделений пуст";
    private const string LoadMessage = "Загрузка...";
    private bool IsShowList => ShowedDivisions.Any();
    protected override Task OnInitializedAsync()
    {
        return LoadDivisions();
    }

    private async Task LoadDivisions()
    {
        Divisions = [];
        Message = LoadMessage;
        try
        {
            Divisions = await DivisionsService.ReadAll(null);
            Message = EmptyListMessage;
        } 
        catch (Exception ex)
        {
            Message = ex.Message;
        }
    }
}
