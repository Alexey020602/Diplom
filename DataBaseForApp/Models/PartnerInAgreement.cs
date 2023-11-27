using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataBase.Models;
[Index("ContactPersons", IsUnique = true)]
[PrimaryKey(nameof(AgreementId), nameof(PartnerInAgreementId))]
public class PartnerInAgreement
{
    [MaxLength(500)]
    public string ContactPersons { get; set; } = null!;
    public int AgreementId { get; set; }
    public Agreement Agreement { get; set; } = null!;
    public int PartnerInAgreementId { get; set; }
    public Partner Partner { get; set; } = null!;
}