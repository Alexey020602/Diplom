using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DataBase.Models;

public class Direction
{
    public int Id { get; set; }
    [MaxLength(200)]
    public string Name { get; set; } = null!;
    [JsonIgnore]
    public ICollection<Partner>? Partners { get; set; }
    [JsonIgnore]
    public ICollection<Division>? Divisions { get; set; }
    [JsonIgnore]
    public ICollection<Interaction>? Interactions { get; set; }

    public override string ToString() => $"{Name}:{Id}";
}