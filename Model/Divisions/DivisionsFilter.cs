namespace Model.Divisions;

public record DivisionsFilter(
    string? ShortName = null, 
    string? FullName = null, 
    int? FacultyId = null,
    int? Skip = null,
    int? Take = null);