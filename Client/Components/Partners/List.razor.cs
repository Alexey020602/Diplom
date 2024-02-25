using Microsoft.AspNetCore.Components;
using SharedModel;
using Client.Services;

namespace Client.Components.Partners;

public partial class List
{
    [Inject] private IPartnersService PartnersService { get; set; } = default!;
    private IEnumerable<PartnerForList>? Partners { get; set; }
    private IEnumerable<PartnerForList> ShowedPartners => Partners?.Where(partner => partner.name.Contains(ShortNameFilter)) ?? [];
 
    private string message = string.Empty;

    private string ShortNameInput { get; set; } = string.Empty;
    private string ShortNameFilter { get; set; } = string.Empty;
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
        try
        {
            Partners = await PartnersService.GetPartners();
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
}
