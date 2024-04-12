using Client.Services;
using DataBase.Models;
using Microsoft.AspNetCore.Components;

namespace Client.Components.Partners;

//public partial class Form: ComponentBase
//{
//    [Inject] private NavigationManager NavigationManager { get; set; } = default!;
//    [Inject] private IPartnerTypesService PartnerTypesService { get; set; } = default!;
//    [Inject] private IDirectionsService DirectionsService { get; set; } = default!;

//    [Parameter]
//    public Partner Partner { get; set; } = null!;
//    [Parameter]
//    public EventCallback OnSubmit { get; set; }
//    [Parameter] public string PreviousScreenUri { get; set; } = string.Empty;

//    private IEnumerable<PartnerType>? partnerTypes;
//    private List<Direction>? directions;
//    protected async override Task OnParametersSetAsync()
//    {
//        var directionsTask = LoadDirections();
//        var partnerTypesTask = LoadPartnerTypes();
//        await directionsTask;
//        await partnerTypesTask;
//    }

//    private async Task LoadDirections()
//    {
//        try
//        {
//            var directions = await DirectionsService.ReadAll();
//            Console.WriteLine(directions);
//            this.directions = directions
//                .Where(direction => !Partner.Directions.Contains(direction))
//                .ToList(); 
//        }
//        catch (Exception ex)
//        {
//            //TODO Добавить обработку исключений
//            Console.WriteLine($"Исключение при загрузке списка направлений: {ex}");
//        }
//    }
//    private async Task LoadPartnerTypes()
//    {
//        Console.WriteLine("Загрузка списка типов партнеров");
//        try
//        {
//            partnerTypes = await PartnerTypesService.ReadAll();
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"Исключение при загрузке списка типов партнеров: {ex}");
//        }
//    }

//    private async Task OnValidSubmit()
//    {
//        Console.WriteLine(Partner);
//        await OnSubmit.InvokeAsync();
//        GoToPreviousScreen();
//    }
//    private void GoToPreviousScreen()
//    {
//        NavigationManager.NavigateTo(PreviousScreenUri);
//    }
//}
