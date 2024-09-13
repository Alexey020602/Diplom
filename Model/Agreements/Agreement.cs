using System.ComponentModel.DataAnnotations;

namespace Model.Agreements;

public class Agreement
{
    public int Id { get; set; }
    [StringLength(15)] 
    [Required(ErrorMessage = "Необходимо ввести номер соглашения")]
    public string Number { get; set; } = string.Empty;
    [Required(ErrorMessage = "Необходимо выбрать тип соглашения")] 
    public AgreementType? Type { get; set; }
    [Required(ErrorMessage = "Необходимо выбрать статус соглашения")] 
    public Status? Status { get; set; }
    public DateOnly Start { get; set; }
    public DateOnly End { get; set; }
    [MinLength(1)] public List<DivisionInAgreement> Divisions { get; set; } = [];
    [MinLength(1)] public List<PartnerInAgreement> Partners { get; set; } = [];

    public override string ToString()
    {
        return $"{Number} {Type} {Start.ToShortDateString()} - {End.ToLongDateString()}";
    }
}