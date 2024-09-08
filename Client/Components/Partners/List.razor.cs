using Client.Services.Api;
using Client.Shared.List;
using Microsoft.AspNetCore.Components;
using Model.Partners;

namespace Client.Components.Partners;

public partial class List : SearchableStyledList<PartnerShort>
{
    [Inject] private IPartnersService PartnersService { get; set; } = default!;
    private PartnerType? PartnerType { get; set; }
    protected override string CreateHref => "partners/create";

    protected override string RowHref(PartnerShort partner)
    {
        return $"partners/{partner.Id}";
    }

    protected override Task<List<PartnerShort>> Load()
    {
        return PartnersService.ReadAll(PartnerType?.Id);
    }

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