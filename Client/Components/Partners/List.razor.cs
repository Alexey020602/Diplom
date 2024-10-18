using Client.Services.Api;
using Client.Shared.List;
using Client.Shared.Select;
using Microsoft.AspNetCore.Components;
using Model;
using Model.Partners;
using Radzen;

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

    protected override string CreateText => "Добавить нового партнера";

    protected override  Task<Paging<PartnerShort>> Load(string? text, int? skip, int? take)
    {
        return PartnersService.ReadAll(new PartnersFilter(
            ShortName: text,
            PartnerTypeId: PartnerType?.Id,
            Skip: skip ?? 0,
            Take: take ?? pageSize
        ));
    }
    protected override void ClearFilterFields()
    {
        PartnerType = null;
    }
}