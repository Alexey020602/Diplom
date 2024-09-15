namespace Model.Partners;

public record PartnerShort(int Id, string Name)
{
    public override string ToString()
    {
        return Name;
    }
}
//public interface PartnerShort
//{
//    int Id { get; set; }
//    string Name { get; set; }
//}