using System.ComponentModel.DataAnnotations;
using Model.Divisions;
using Model.Partners;

namespace Model.Interactions;

public class Interaction
{
    public int Id { get; set; }
    public PartnerShort? Partner { get; set; }
    public DivisionShort? Division { get; set; }
    [Required] public Type? Type { get; set; }
    [StringLength(500)] public string Theme { get; set; } = string.Empty;
    [StringLength(9)] public string ContactCode { get; set; } = string.Empty;
    public DateTime SigningDate { get; set; }
    public DateTime Begin { get; set; }
    public DateTime End { get; set; }
    [MinLength(1)] public List<Direction> Directions { get; set; } = [];

    public override string ToString() => $"{ContactCode} типа {Type} от {SigningDate}, {Begin} - {End}";
}