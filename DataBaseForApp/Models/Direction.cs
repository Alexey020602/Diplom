using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DataBase.Models;

/// <summary>
/// Класс модели для сущности "Направление"
/// </summary>
public class Direction
{
    /// <summary>
    /// Идентификатор направления
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Название направления
    /// </summary>
    [StringLength(200)] public string Name { get; set; } = null!;
    /// <summary>
    /// Навигационное свойство с сущностью "Партнер"
    /// </summary>
    public ICollection<Partner> Partners { get; set; } = [];
    /// <summary>
    /// Навигационное свойство с сущностью "Подразделение"
    /// </summary>
    public ICollection<Division> Divisions { get; set; } = [];
    /// <summary>
    /// Навигационное свойство с сущностью "Взаимодействие"
    /// </summary>
    public ICollection<Interaction> Interactions { get; set; } = [];
}