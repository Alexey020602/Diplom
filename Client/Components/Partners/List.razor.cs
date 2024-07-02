using Microsoft.AspNetCore.Components;
using Client.Services;
using Client.Shared.List;
using Model.Partners;
using Type = Model.Partners.Type;

namespace Client.Components.Partners;

public partial class List: SearchableStyledList<PartnerShort>
{
    [Inject] private IPartnersService PartnersService { get; set; } = default!;
    private Type? PartnerType { get; set; }
    protected override string CreateHref => "partners/create";
    protected override string RowHref(PartnerShort partner) => $"partners/{partner.Id}";
    protected override Task<List<PartnerShort>> Load() => PartnersService.ReadAll(PartnerType?.Id);
    private Task SelectPartnerType(Type? partnerType)
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