using System.ComponentModel.DataAnnotations;
using Division = Model.Divisions.DivisionShort;
namespace Model.Agreements;

public class Agreement
{
    public int Id { get; set; }
    [StringLength(15)] public string Number { get; set; } = string.Empty;
    public Type? Type { get; set; }
    public Status? Status { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    [MinLength(1)] public List<Division> Divisions { get; set; } = [];
    [MinLength(1)] public List<Partner> Partners { get; set; } = [];
    public override string ToString() => $"{Number} {Type} {Start} - {End}";
}