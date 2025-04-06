using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DataBase.Models;

/// <summary>
/// Класс модели для сущности "Тип взаимодействия"
/// </summary>
public class InteractionType
{
    /// <summary>
    /// Идентификатор типа взаимодействия
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Название типа взаимодействия
    /// </summary>
    [MaxLength(100)] public string Name { get; set; } = string.Empty;
    /// <summary>
    /// Навигационное свойство с сущностью "Взаимодействие"
    /// </summary>
    public ICollection<Interaction> Interactions { get; set; } = [];
}