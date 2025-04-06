using System.ComponentModel.DataAnnotations;

namespace DataBase.Models;

/// <summary>
/// Класс модели для сущности "Взаимодействие"
/// </summary>
public class Interaction
{
    /// <summary>
    /// Идентификатор взаимодействия
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Навигационное свойство с сущностью "Партнер"
    /// </summary>
    public Partner Partner { get; set; } = null!;
    /// <summary>
    /// Навигационное свойство с сущностью "Подразделение"
    /// </summary>
    public Division Division { get; set; } = null!;
    /// <summary>
    /// Навигационное свойство с сущностью "Тип взаимодействия"
    /// </summary>
    public InteractionType InteractionType { get; set; } = null!;
    /// <summary>
    /// Тема взаимодействия
    /// </summary>
    [MaxLength(500)] public string Theme { get; set; } = null!;
    /// <summary>
    /// Шифр договора
    /// </summary>
    [MaxLength(9)] public string ContactCode { get; set; } = null!;
    /// <summary>
    /// Дата подписания договора
    /// </summary>
    public DateTime SigningDateTime { get; set; }
    /// <summary>
    /// Дата начала действия договора
    /// </summary>
    public DateTime BeginigDateTime { get; set; }
    /// <summary>
    /// Дата окончания действия договора
    /// </summary>
    public DateTime EndingDateTime { get; set; }
    /// <summary>
    /// Навигационное свойство с сущностью "Направление"
    /// </summary>
    public List<Direction> Directions { get; set; } = [];

    public override string ToString()
    {
        return $"{ContactCode} {InteractionType} от {SigningDateTime.ToShortDateString()}, {BeginigDateTime.ToShortDateString()} - {EndingDateTime.ToShortDateString()}";
    }
}