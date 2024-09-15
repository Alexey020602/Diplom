using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace DataBase.Models;

[Index("AgreementNumber", IsUnique = true)]
public class Agreement
{
    public int Id { get; set; }
    [MaxLength(15)] public string AgreementNumber { get; set; } = null!;
    public DateTime StarDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    [MinLength(1)] public List<DivisionInAgreement> DivisionInAgreements { get; set; } = [];
    [MinLength(1)] public List<PartnerInAgreement> PartnerInAgreements { get; set; } = [];
    public AgreementType AgreementType { get; set; } = null!;
    public AgreementStatus AgreementStatus { get; set; } = null!;

    public override string ToString()
    {
        return
            $"{AgreementNumber} {AgreementType} {StarDateTime.ToShortDateString()} - {EndDateTime.ToShortDateString()}";
    }
}