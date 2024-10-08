using Client.Services.Api;
using Microsoft.AspNetCore.Components;
using Model.Agreements;
using Radzen;
using AgreementStatus = Model.Agreements.Status;

namespace Client.Components.Agreements;

public partial class List
{
    private AgreementStatus? agreementStatus;
    private AgreementType? agreementType;
    [Inject] private IAgreementService AgreementService { get; set; } = null!;
    protected override string CreateHref => "/agreements/create";
    protected override string CreateText => "Добавить соглашение";
    protected override Task<List<AgreementShort>> Load()
    {
        return AgreementService.ReadAll(agreementType?.Id, agreementStatus?.Id);
    }

    protected override string RowHref(AgreementShort item)
    {
        return $"/agreements/{item.Id}";
    }

    protected override void ClearFilterFields()
    {
        agreementStatus = null;
        agreementType = null;
    }
}