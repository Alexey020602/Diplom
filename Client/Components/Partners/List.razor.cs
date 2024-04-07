using Microsoft.AspNetCore.Components;
using DataBase.Models;
using Client.Services;
using SharedModel;
using Client.Shared.List;

namespace Client.Components.Partners;

public partial class List: SearchableStyledList<PartnerShort>
{
    [Inject] private IPartnersService PartnersService { get; set; } = default!;
    private PartnerType? PartnerType { get; set; }
    protected override string CreateHref => "partners/create";
    protected override string RowHref(PartnerShort partner) => $"partners/{partner.Id}";
    //private async Task LoadPartners()
    //{
    //    Partners = [];
    //    Console.WriteLine("Загрузка партнеров");
    //    try
    //    {
    //        Partners = await Load();
    //        message = string.Empty;
    //    }
    //    catch (Exception ex)
    //    {
    //        message = $"Поймали исключение на списке партнеров:\n{ex}";
    //    }
    //}

    protected override Task<List<PartnerShort>> Load() => PartnersService.ReadAll(PartnerType?.Id);
    //protected override async Task OnInitializedAsync()
    //{
    //    Console.WriteLine("Создан список партнеров");
    //    message = "Загрузка...";
    //    await LoadPartners();
    //}

    private Task SelectPartnerType(PartnerType? partnerType)
    {
        PartnerType = partnerType;
        return LoadItems();
    }

    private Task ResetSelectedPartnerType()
    {
        PartnerType = null;
        return LoadItems();
    }
}