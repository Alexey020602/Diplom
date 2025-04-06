using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace DataBase.Models;

/// <summary>
/// Класс модели для сущности "Статус соглашения"
/// </summary>
[Index(nameof(Name), IsUnique = true)]
public class AgreementStatus
{
    /// <summary>
    /// Идентификатор статуса соглашения
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Название статуса соглашения
    /// </summary>
    [MaxLength(13)] public string Name { get; set; } = null!;
    /// <summary>
    /// Навигационное свойство с сущностью "Соглашение"
    /// </summary>
    public ICollection<Agreement> Agreements { get; set; } = null!;

    public override string ToString()
    {
        return Name;
    }
}