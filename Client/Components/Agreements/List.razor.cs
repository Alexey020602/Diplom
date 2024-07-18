using Client.Services;
using Microsoft.AspNetCore.Components;
using Model.Agreements;
using AgreementStatus = Model.Agreements.Status;

namespace Client.Components.Agreements;

public partial class List
{
    private AgreementType? agreementType = null;
    private AgreementStatus? agreementStatus = null;
    [Inject] private IAgreementService AgreementService { get; set; } = null!;
    protected override string CreateHref => "/agreements/create";
    protected override Task<List<AgreementShort>> Load() => AgreementService.ReadAll(agreementType?.Id, agreementStatus?.Id); 

    protected override string RowHref(AgreementShort item) => $"/agreements/{item.Id}";

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
