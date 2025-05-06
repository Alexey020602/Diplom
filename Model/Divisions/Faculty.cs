namespace Model.Divisions;

public class Faculty: IEquatable<Faculty>
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;

    public override string ToString()
    {
        return Name;
    }

    public override bool Equals(object? obj) => obj is Faculty other && Equals(other);

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public bool Equals(Faculty? other)
    {
        return other != null && other.Id == Id;
    }
}