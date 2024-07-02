using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
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
    [JsonIgnore]
    public string Text { get => ContactPersons; set => ContactPersons = value; }
    [JsonIgnore]
    public string NonInputDescription => Name;
}