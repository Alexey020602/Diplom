namespace Model.Agreements;

public record AgreementsFilter(
    string? Number = null, 
    int? AgreementTypeId = null, 
    int? AgreementStatusId = null,
    DateOnly? StartDate = null,
    DateOnly? EndDate = null,
    int? Skip = null,
    int? Take = null);