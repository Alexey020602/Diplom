using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace DataBase.Models;

[PrimaryKey(nameof(AgreementId), nameof(PartnerId))]
public class PartnerInAgreement
{
    [MaxLength(500)] public string ContactPersons { get; set; } = null!;

    public int AgreementId { get; set; }
    public Agreement Agreement { get; set; } = null!;
    public int PartnerId { get; set; }
    public Partner Partner { get; set; } = null!;
}