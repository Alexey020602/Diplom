using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataBase.Models;
[Index("AgreementNumber", IsUnique = true),]
public class Agreement
{
    public int Id { get; set; }
    [MaxLength(15)] public string AgreementNumber { get; set; }
    
    // [Column(TypeName = "date")]
    public DateTime StarDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    public ICollection<DivisionInAgreement> DivisionInAgreements { get; set; }
    public ICollection<PartnerInAgreement> PartnerInAgreements { get; set; }
    public AgreementType AgreementType { get; set; }
    public AgreementStatus AgreementStatus { get; set; }
}