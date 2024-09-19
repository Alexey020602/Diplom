using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Model.Agreements;

public class PartnerInAgreement : ISelectionWithTextElement
{
    public required int Id { get; init; }
    public required string Name { get; init; }
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
    
    

    public override bool Equals(object? obj)
    {
        
        if (obj is not PartnerInAgreement other) return false;
        
        return Id == other.Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}