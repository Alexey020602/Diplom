using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Model.Agreements;

public class PartnerInAgreement : ISelectionWithTextElement
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    [StringLength(500)] public string ContactPersons { get; set; } = string.Empty;

    [JsonIgnore]
    public string Text
    {
        get => ContactPersons;
        set => ContactPersons = value;
    }

    [JsonIgnore] public string NonInputDescription => Name;

    public override string ToString()
    {
        return $"""
                {Name}
                {ContactPersons}
                """;
    }
}