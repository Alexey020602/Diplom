namespace Model;

public class InteractionInRelationship
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;

    public override string ToString()
    {
        return Description;
    }
}