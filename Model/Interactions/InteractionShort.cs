namespace Model.Interactions;

public record InteractionShort(int Id, string Name)
{
    public override string ToString()
    {
        return Name;
    }
}