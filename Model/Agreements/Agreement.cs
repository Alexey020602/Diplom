using System.ComponentModel.DataAnnotations;

namespace Model.Agreements;

public class Agreement
{
    public int Id { get; set; }
    [StringLength(15)] [Required] public string Number { get; set; } = string.Empty;
    [Required] public AgreementType? Type { get; set; }
    [Required] public Status? Status { get; set; }
    public DateTime Start { get; set; } = DateTime.Now;
    public DateTime End { get; set; } = DateTime.Now;
    [MinLength(1)] public List<DivisionInAgreement> Divisions { get; set; } = [];
    [MinLength(1)] public List<PartnerInAgreement> Partners { get; set; } = [];

    public override string ToString()
    {
        return $"{Number} {Type} {Start.ToShortDateString()} - {End.ToLongDateString()}";
    }
}