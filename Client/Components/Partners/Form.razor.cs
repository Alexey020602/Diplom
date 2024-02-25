using Client.Services;
using DataBase.Models;
using Microsoft.AspNetCore.Components;

namespace Client.Components.Partners;

public partial class Form: ComponentBase
{
    [Inject]
    private IPartnerTypesService PartnerTypesService { get; set; } = default!;
    [Inject]
    private IDirectionsService DirectionsService { get; set; } = default!;

    [Parameter]
    public Partner Partner { get; set; } = null!;
    [Parameter]
    public EventCallback OnSubmit { get; set; }

    private IEnumerable<PartnerType>? partnerTypes;
    private List<Direction>? directions;
    private string PartnerTypeId => Partner.PartnerTypeId != 0 ? Partner.PartnerTypeId.ToString() : "Выберите опцию";


    protected async override Task OnParametersSetAsync()
    {
        var directionsTask = LoadDirections();
        var partnerTypesTask = LoadPartnerTypes();
        await directionsTask;
        await partnerTypesTask;
    }

    private async Task LoadDirections()
    {
        try
        {
            var directions = await DirectionsService.GetDirections();
            Console.WriteLine(directions);
            this.directions = directions
                .Where(direction => !Partner.Directions.Contains(direction))
                .ToList(); 
        }
        catch (Exception ex)
        {
            //TODO Добавить обработку исключений
            Console.WriteLine($"Исключение при загрузке списка направлений: {ex}");
        }
    }

    private void RemoveSelectedDirections()
    {
        if (directions is null) return;

        foreach (var direction in Partner.Directions)
            directions.Remove(direction);
    }

    private async Task LoadPartnerTypes()
    {
        Console.WriteLine("Загрузка списка типов партнеров");
        try
        {
            partnerTypes = await PartnerTypesService.GetPartnerTypesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Исключение при загрузке списка типов партнеров: {ex}");
        }
    }

    private void OnPartnerTypeSelected(string id)
    {
        Console.WriteLine($"Выбран тип партнера {id}");
        var partnerTypeId = int.Parse(id);
        Partner.PartnerTypeId = partnerTypeId;
    }

    private async Task OnValidSubmit()
    {
        Console.WriteLine(Partner);
        await OnSubmit.InvokeAsync();
    }
}
