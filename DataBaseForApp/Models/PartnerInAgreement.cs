using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace DataBase.Models;

/// <summary>
/// Класс модели для сущности "Партнер в соглашении"
/// </summary>
[PrimaryKey(nameof(AgreementId), nameof(PartnerId))]
public class PartnerInAgreement
{
    /// <summary>
    /// Контактные лица от партнера
    /// </summary>
    [MaxLength(500)] public string ContactPersons { get; set; } = null!;
    /// <summary>
    /// Идентификатор соглашения
    /// </summary>
    public int AgreementId { get; set; }
    /// <summary>
    /// Навигационное свойство с сущность "Соглашение"
    /// </summary>
    public Agreement Agreement { get; set; } = null!;
    /// <summary>
    /// Идентификатор партнера
    /// </summary>
    public int PartnerId { get; set; }
    /// <summary>
    /// Навигационное свойство с сущностью "Партнер"
    /// </summary>
    public Partner Partner { get; set; } = null!;
}