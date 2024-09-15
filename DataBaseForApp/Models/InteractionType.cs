using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DataBase.Models;

public class InteractionType
{
    public int Id { get; set; }
    [MaxLength(100)] public string Name { get; set; } = string.Empty;
    [JsonIgnore] public ICollection<Interaction> Interactions { get; set; } = [];

    public override string ToString()
    {
        return $"{Name}";
    }
}