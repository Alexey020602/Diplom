using Client.Services.Api;
using Microsoft.AspNetCore.Components;
using Model;
using Model.Agreements;
using Radzen;
using AgreementStatus = Model.Agreements.Status;

namespace Client.Components.Agreements;

public partial class List
{
    private AgreementStatus? agreementStatus;
    private AgreementType? agreementType;
    private DateTime? startDate;
    private DateTime? endDate;
    private DateOnly? StartDateOnly => startDate != null ? DateOnly.FromDateTime(startDate.Value) : null;
    private DateOnly? EndDateOnly => endDate != null ? DateOnly.FromDateTime(endDate.Value) : null;
    [Inject] private IAgreementService AgreementService { get; set; } = null!;
    protected override string CreateHref => "/agreements/create";
    protected override string CreateText => "Добавить соглашение";
    protected override Task<Paging<AgreementShort>> Load(string? text, int? skip, int? take)
    {
        return AgreementService.ReadAll(new AgreementsFilter(
            Number: text,
            AgreementTypeId: agreementType?.Id,
            AgreementStatusId: agreementStatus?.Id,
            StartDate: StartDateOnly,
            EndDate: EndDateOnly,
            Skip: skip,
            Take: take));
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