using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataBase.Models;
[PrimaryKey(nameof(DivisionInAgreementId), nameof(AgreementId))]
public class DivisionInAgreement
{
    [MaxLength(100)] 
    public string Name { get; set; } = null!;
    public int DivisionInAgreementId { get; set; }
    public Division Division { get; set; } = null!;
    public int AgreementId { get; set; }
   public Agreement Agreement { get; set; } = null!;
   public string ContactPersons { get; set; } = string.Empty;
}