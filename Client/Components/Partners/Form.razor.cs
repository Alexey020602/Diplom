using Client.Services;
using DataBase.Models;
using Microsoft.AspNetCore.Components;

namespace Client.Components.Partners;

public partial class Form: ComponentBase
{
    [Inject]
    private IPartnerTypesService PartnerTypesService { get; set; }
    [Inject]
    private IDirectionsService DirectionsService { get; set; }

    [Parameter]
    public Partner Partner { get; set; } = null!;
    [Parameter]
    public EventCallback OnSubmit { get; set; }

    private IEnumerable<PartnerType>? partnerTypes;
    private List<Direction>? directions;
    private string PartnerTypeId => Partner.PartnerTypeId != 0 ? Partner.PartnerTypeId.ToString() : "Выберите опцию";


    protected async override Task OnParametersSetAsync()
    {
        SetDirectionsToPartner();
        var directionsTask = LoadDirections();
        var partnerTypesTask = LoadPartnerTypes();
        await directionsTask;
        await partnerTypesTask;
    }

    private void SetDirectionsToPartner()
    {
        if (Partner.Directions is not null) return;
        Partner.Directions = [];
    }

    private async Task LoadDirections()
    {
        try
        {
            directions = await DirectionsService.GetDirections();
        }
        catch (Exception ex)
        {
            //TODO Добавить обработку исключений
            Console.WriteLine($"Исключение при загрузке списка направлений: {ex}");
        }
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
