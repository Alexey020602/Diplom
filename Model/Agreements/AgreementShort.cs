namespace Model.Agreements;

public record class AgreementShort(int Id, string Name)
{
    public override string ToString() => Name;
}
