using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DataBase.Models;

/// <summary>
/// Класс модели для сущности "Факультет"
/// </summary>
public class Faculty
{
    /// <summary>
    /// Идентификатор факультета
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Название факультета
    /// </summary>
    [StringLength(100)] public string Name { get; set; } = null!;
    /// <summary>
    /// Навигационное свойство с сущностью "Подразделение"
    /// </summary>
    public ICollection<Division> Divisions { get; set; } = [];
}