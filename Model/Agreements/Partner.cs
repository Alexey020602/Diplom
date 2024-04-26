using System.ComponentModel.DataAnnotations;

namespace Model.Agreements;

public class Partner
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    [StringLength(500)] public string ContactPersons { get; set; } = string.Empty;
    public override string ToString() =>
        $"""
        {Name}
        {ContactPersons}
        """;
}