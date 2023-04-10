using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataBase.Models;
[Index("ShortName", IsUnique = true)]
public class Division
{
    public int Id { get; set; }
    
    [MaxLength(200)]
    public string FullName { get; set; }
    
    [MaxLength(50)]
    public string ShortName { get; set; }
    public Faculty Faculty { get; set; }
    
    [MaxLength(500)]
    public string? Contacts { get; set; }
    
    [MaxLength(100)]
    public string? Site { get; set; }
    public ICollection<DivisionInAgreement> DivisionsInAgreement { get; set; }
    public ICollection<Interaction> Interactions { get; set; }
    public ICollection<Direction> Directions { get; set; }
}