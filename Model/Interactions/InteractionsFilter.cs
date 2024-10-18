namespace Model.Interactions;

public record InteractionsFilter(
    string? Code = null,
    int? InteractionTypeId = null, 
    DateOnly? Sign = null, 
    DateOnly? Start = null,
    DateOnly? End = null, 
    int? Skip = null,
    int? Take = null);