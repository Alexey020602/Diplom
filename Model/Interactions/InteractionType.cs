namespace Model.Interactions;

public class InteractionType
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public override string ToString()
    {
        return Name;
    }
}