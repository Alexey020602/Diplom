using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace DataBase.Models;

/// <summary>
/// Класс модели для сущности "Тип соглашения"
/// </summary>
[Index(nameof(Name), IsUnique = true)]
public class AgreementType
{
    /// <summary>
    /// Идентификатор типа соглашения
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Название типа соглашения
    /// </summary>
    [StringLength(100)] public string Name { get; set; } = null!;
    /// <summary>
    /// Навигационное свойство с сущностью "Соглашение"
    /// </summary>
    public ICollection<Agreement> Agreements { get; set; } = null!;

    public override string ToString()
    {
        return $"{Name}";
    }
}