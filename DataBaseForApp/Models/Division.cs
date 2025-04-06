using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace DataBase.Models;

/// <summary>
/// Класс модели для сущности "Подразделение"
/// </summary>
[Index(nameof(ShortName), IsUnique = true)]
public class Division
{
    /// <summary>
    /// Идентификатор подразделения
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Полное название подразделения
    /// </summary>
    [MaxLength(200)] public string FullName { get; set; } = null!;
    /// <summary>
    /// Краткое название подразделения
    /// </summary>
    [MaxLength(50)] public string ShortName { get; set; } = null!;
    /// <summary>
    /// Навигацинное свойство с сущностью "Факультет"
    /// </summary>
    public Faculty Faculty { get; set; } = null!;
    /// <summary>
    /// Контактные данные
    /// </summary>
    [MaxLength(500)] public string? Contacts { get; set; }
    /// <summary>
    /// Сайт подразделения
    /// </summary>
    [MaxLength(100)] public string? Site { get; set; }
    /// <summary>
    /// Навигационное свойство с сущностью "Подразделение в соглашении"
    /// </summary>
    public List<DivisionInAgreement> DivisionsInAgreement { get; set; } = [];
    /// <summary>
    /// Навигационное свойство с сущностью "Взаимодействие"
    /// </summary>
    public List<Interaction> Interactions { get; set; } = [];
    /// <summary>
    /// Навигационное свойство с сущностью "Направление"
    /// </summary>
    public List<Direction> Directions { get; set; } = [];
}