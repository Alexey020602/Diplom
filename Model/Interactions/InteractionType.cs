namespace Model.Interactions;

public class InteractionType: IEquatable<InteractionType>
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;

    public override string ToString()
    {
        return Name;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public override bool Equals(object? other) => other is InteractionType type && Equals(type);
    public bool Equals(InteractionType? other)
    {
        return other != null && other.Id == Id;
    }
}