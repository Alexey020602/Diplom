using DataBase.Models;
using Microsoft.AspNetCore.Components;
using SharedModel;

namespace Client.Components.Partners;

public partial class List
{
    IEnumerable<PartnerForList> Partners { get; set; } = new List<PartnerForList>();

    private string message = "Загрузка...";

    async Task LoadPartners()
    {
        Console.WriteLine("Начата загрузка списка партнеров");
        try
        {
            Partners = await PartnersService.GetPartners();
        }
        catch (Exception ex)
        {
            message = $"Поймали исключение на списке партнеров: {ex}";
        }
    }

    void OpenPartnerDetails(Partner partner)
    {
        NavigationManager.NavigateTo("/person/" + partner.Id.ToString());
    }

    protected override async Task OnInitializedAsync()
    {
        Console.WriteLine("On init");
        await LoadPartners();
    }
}
