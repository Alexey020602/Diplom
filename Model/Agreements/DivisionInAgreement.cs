using System.Text.Json.Serialization;

namespace Model.Agreements;

public class DivisionInAgreement: ISelectionWithTextElement
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public string ContactPersons { get; set; } = string.Empty;
    [JsonIgnore] public string Text
    {
        get => ContactPersons;
        set => ContactPersons = value;
    }

    public override string ToString() => $"{Description}\n{ContactPersons}";
    [JsonIgnore] public string NonInputDescription => Description;
}