namespace Client.Shared.Select;

sealed class RadzenDropDownEqualityComparer<T>: EqualityComparer<object> where T: IEquatable<T>
{
    public override bool Equals(object? x, object? y)
    {
        if (x is not T xItem || y is not T yItem) return false;
        return xItem.Equals(yItem);
    }
        
    public override int GetHashCode(object obj) => obj.GetHashCode();
}