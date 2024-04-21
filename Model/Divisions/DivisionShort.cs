namespace Model.Divisions;
public record DivisionShort(int Id, string Name)
{
    public override string ToString() => Name;
}
