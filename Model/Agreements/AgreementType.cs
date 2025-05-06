namespace Model.Agreements;

public class AgreementType: IEquatable<AgreementType>
{
    public int Id { get; init; }
    public string Name { get; set; } = string.Empty;

    public override string ToString()
    {
        return Name;
    }

    public override bool Equals(object? obj)
    {
        return obj is AgreementType other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public bool Equals(AgreementType? other)
    {
        return other != null && other.Id == Id;
    }
}