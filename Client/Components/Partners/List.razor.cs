using Microsoft.AspNetCore.Components;
using DataBase.Models;
using Client.Services;

namespace Client.Components.Partners;

public partial class List
{
    [Inject] private IPartnersService PartnersService { get; set; } = default!;
    private IEnumerable<Partner>? Partners { get; set; }
    private PartnerType? PartnerType { get; set; }
    private IEnumerable<Partner> ShowedPartners => from partner in Partners ?? []
                                                          where partner.ShortName.Contains(ShortNameFilter)
                                                          orderby partner.ShortName
                                                          select partner;
        //Partners?.Where(partner => partner.name.Contains(ShortNameFilter)) ?? [];
    private string message = string.Empty;
    private string ShortNameInput { get; set; } = string.Empty;
    private string ShortNameFilter { get; set; } = string.Empty;
    private bool ShowPartners => Partners != null && Partners.Any();
    private void ClearFilterField()
    {
        ShortNameInput = string.Empty;
        ShortNameFilter = string.Empty;
    }
    private void SetFilterField()
    {
        ShortNameFilter = ShortNameInput;
    }
    private async Task LoadPartners()
    {
        Partners = [];
        Console.WriteLine("Загрузка партнеров");
        try
        {
            Partners = await PartnersService.GetPartners(PartnerType?.Id);
            message = string.Empty;
        }
        catch (Exception ex)
        {
            message = $"Поймали исключение на списке партнеров: {ex}";
        }
    }
    protected override async Task OnInitializedAsync()
    {
        message = "Загрузка...";
        await LoadPartners();
    }

    private Task SelectPartnerType(PartnerType? partnerType)
    {
        PartnerType = partnerType;
        return LoadPartners();
    }

    private Task ResetSelectedPartnerType()
    {
        PartnerType = null;
        return LoadPartners();
    }
}