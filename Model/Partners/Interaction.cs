namespace Model.Partners;

public class Interaction
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;

    public override string ToString() => Description;
}