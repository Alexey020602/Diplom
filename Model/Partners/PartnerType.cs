using System.ComponentModel.DataAnnotations;

namespace Model.Partners;

public class PartnerType
{
    public int Id { get; set; }
    [StringLength(50)] public string Name { get; set; } = string.Empty;
    public override string ToString() => Name;
}