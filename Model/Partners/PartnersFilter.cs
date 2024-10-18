namespace Model;

public record PartnersFilter(
    string? ShortName = null,
    string? FullName = null,
    int? PartnerTypeId = null,
    int? DirectionId = null,
    int? Skip = null,
    int? Take = null);