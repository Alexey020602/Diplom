using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace DataBase.Models;

/// <summary>
///     Класс модели для сущности "Тип партнера"
/// </summary>
[Index(nameof(Name), IsUnique = true)]
public class PartnerType
{
    /// <summary>
    ///     Идентификатор типа партнера
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///     Название типа партнера
    /// </summary>
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    ///     Список партнеров соответсвующего типа
    /// </summary>
    [JsonIgnore]
    public ICollection<Partner> Partners { get; set; } = [];

    public override string ToString()
    {
        return Name;
    }
}