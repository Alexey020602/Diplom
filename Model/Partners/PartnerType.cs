using System.ComponentModel.DataAnnotations;

namespace Model.Partners;

public class PartnerType: IEquatable<PartnerType>
{
    public int Id { get; init; }
    [StringLength(50)] public string Name { get; init; } = string.Empty;

    public override string ToString() => Name;

    public override bool Equals(object? obj) => obj is PartnerType other && Equals(other);
    public bool Equals(PartnerType? other) => other != null && other.Id == Id;
    public override int GetHashCode() => Id.GetHashCode();
}