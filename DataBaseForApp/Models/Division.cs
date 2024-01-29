using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace DataBase.Models;
[Index("ShortName", IsUnique = true)]
public class Division
{
    public int Id { get; set; }
    
    [MaxLength(200)]
    public string FullName { get; set; } = null!;

    [MaxLength(50)]
    public string ShortName { get; set; } = null!;
    public Faculty Faculty { get; set; } = null!;

    [MaxLength(500)]
    public string? Contacts { get; set; }
    
    [MaxLength(100)]
    public string? Site { get; set; }
    [JsonIgnore]
    public ICollection<DivisionInAgreement> DivisionsInAgreement { get; set; } = null!;
    [JsonIgnore]
    public ICollection<Interaction> Interactions { get; set; } = null!;
    [JsonIgnore]
    public ICollection<Direction> Directions { get; set; } = null!;
}