using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace DataBase.Models;

[Index("ShortName", IsUnique = true)]
public class Division
{
    public int Id { get; set; }

    [MaxLength(200)] public string FullName { get; set; } = null!;

    [MaxLength(50)] public string ShortName { get; set; } = null!;

    public Faculty Faculty { get; set; } = null!;

    [MaxLength(500)] public string? Contacts { get; set; }

    [MaxLength(100)] public string? Site { get; set; }

    [JsonIgnore] public List<DivisionInAgreement> DivisionsInAgreement { get; set; } = [];
    [JsonIgnore] public List<Interaction> Interactions { get; set; } = [];
    public List<Direction> Directions { get; set; } = [];
}