using System.ComponentModel.DataAnnotations;
using Client.Shared.Select;

namespace Model.Agreements;

public class Partner: ISelectionWithTextElement
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    [StringLength(500)] public string ContactPersons { get; set; } = string.Empty;
    public override string ToString() =>
        $"""
        {Name}
        {ContactPersons}
        """;
    public string Text { get => ContactPersons; set => ContactPersons = value; }
    public string NonInputDescription => Name;
}