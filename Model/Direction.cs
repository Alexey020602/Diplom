using System.ComponentModel.DataAnnotations;

namespace Model;

public class Direction: IEquatable<Direction>
{
    public int Id { get; init;  }
    [StringLength(200)] public string Name { get; set; } = string.Empty;

    public override string ToString()
    {
        return Name;
    }

    public bool Equals(Direction? other) => other != null && other.Id == Id;
    public override bool Equals(object? obj)
    {
        return obj is Direction other && Equals(other);
    }

    public override int GetHashCode() => Id.GetHashCode();
}