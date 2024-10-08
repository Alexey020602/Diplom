using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace DataBase.Models;

[Index("Name", IsUnique = true)]
public class AgreementStatus
{
    public int Id { get; set; }
    [MaxLength(13)] public string Name { get; set; } = null!;

    [JsonIgnore] public ICollection<Agreement> Agreements { get; set; } = null!;

    public override string ToString()
    {
        return Name;
    }
}