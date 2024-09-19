using System.Text.Json.Serialization;

namespace Model.Agreements;

public class DivisionInAgreement : ISelectionWithTextElement
{
    public required int Id { get; init; }
    public required string Description { get; init; }
    public string ContactPersons { get; set; } = string.Empty;

    [JsonIgnore]
    public string Text
    {
        get => ContactPersons;
        set => ContactPersons = value;
    }

    [JsonIgnore] public string NonInputDescription => Description;

    public override string ToString()
    {
        return $"{Description}\n{ContactPersons}";
    }
    
    

    public override bool Equals(object? obj)
    {
        
        if (obj is not DivisionInAgreement other) return false;
        
        return Id == other.Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}