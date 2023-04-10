using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataBase.Models;
[PrimaryKey(nameof(DivisionInAgreementId), nameof(AgreementId))]
public class DivisionInAgreement
{
    [MaxLength(100)] public string Name { get; set; }
    public int DivisionInAgreementId { get; set; }
    public Models.Division Division { get; set; }
   public int AgreementId { get; set; }
   public Agreement Agreement { get; set; }
}