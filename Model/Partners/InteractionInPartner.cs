namespace Model.Partners;

public class InteractionInPartner
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;

    public override string ToString() => Description;
}