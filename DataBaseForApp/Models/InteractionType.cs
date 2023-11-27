using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DataBase.Models;

public class InteractionType
{
    public int Id { get; set; }
    
    [MaxLength(100)]
    public string Name { get; set; }
    [JsonIgnore]
    public ICollection<Interaction> Interactions { get; set; } = null!;
}