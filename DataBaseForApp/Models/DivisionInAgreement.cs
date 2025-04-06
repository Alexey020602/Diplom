using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace DataBase.Models;

/// <summary>
/// Класс модели для сущности "Подразделение в соглашении"
/// </summary>
[PrimaryKey(nameof(DivisionId), nameof(AgreementId))]
public class DivisionInAgreement
{
    /// <summary>
    /// Идентификатор подразделения
    /// </summary>
    public int DivisionId { get; set; }
    /// <summary>
    /// Навигационное свойство с сущностью "Подразделение"
    /// </summary>
    public Division Division { get; set; } = null!;
    /// <summary>
    /// Идентификатор соглашения
    /// </summary>
    public int AgreementId { get; set; }
    /// <summary>
    /// Навигационное свойство с сущностью "Соглашение"
    /// </summary>
    public Agreement Agreement { get; set; } = null!;
    /// <summary>
    /// Контактные данные
    /// </summary>
    [MaxLength(500)] public string ContactPersons { get; set; } = string.Empty;
}