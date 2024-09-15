using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace DataBase.Models;

[PrimaryKey(nameof(DivisionId), nameof(AgreementId))]
public class DivisionInAgreement
{
    public int DivisionId { get; set; }
    public Division Division { get; set; } = null!;
    public int AgreementId { get; set; }
    public Agreement Agreement { get; set; } = null!;
    [MaxLength(500)] public string ContactPersons { get; set; } = string.Empty;
}