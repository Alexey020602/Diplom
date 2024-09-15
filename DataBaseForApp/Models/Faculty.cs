using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DataBase.Models;

public class Faculty
{
    public int Id { get; set; }

    [StringLength(100)] public string Name { get; set; } = null!;

    [JsonIgnore] public ICollection<Division> Divisions { get; set; } = [];

    public override string ToString()
    {
        return Name;
    }
}