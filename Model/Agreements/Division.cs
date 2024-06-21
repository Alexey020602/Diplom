using Client.Shared.Select;

namespace Model.Agreements;

public class Division: ISelectionWithTextElement
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public string ContactPersons { get; set; } = string.Empty;

    public string Text
    {
        get => ContactPersons;
        set => ContactPersons = value;
    }

    public override string ToString() => $"{Description}\n{ContactPersons}";
    public string NonInputDescription => Description;
}