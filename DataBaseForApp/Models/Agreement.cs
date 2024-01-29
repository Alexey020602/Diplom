using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace DataBase.Models;
[Index("AgreementNumber", IsUnique = true),]
public class Agreement
{
    public int Id { get; set; }
    [MaxLength(15)] public string AgreementNumber { get; set; } = null!;
    
    // [Column(TypeName = "date")]
    public DateTime StarDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    public ICollection<DivisionInAgreement> DivisionInAgreements { get; set; } = null!;
    public ICollection<PartnerInAgreement> PartnerInAgreements { get; set; } = null!;
    public AgreementType AgreementType { get; set; } = null!;
    public AgreementStatus AgreementStatus { get; set; } = null!;
}