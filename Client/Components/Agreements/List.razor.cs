using Client.Services.Api;
using Microsoft.AspNetCore.Components;
using Model.Agreements;
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

    private Task Select(AgreementType? agreementType)
    {
        this.agreementType = agreementType;
        return LoadItems();
    }

    private Task Select(AgreementStatus? agreementStatus)
    {
        this.agreementStatus = agreementStatus;
        return LoadItems();
    }
}