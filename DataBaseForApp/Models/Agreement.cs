using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace DataBase.Models;
/// <summary>
/// Класс модели для сущности "Соглашение"
/// </summary>
[Index(nameof(AgreementNumber), IsUnique = true)]
public class Agreement
{
    /// <summary>
    /// Идентификатор соглашения
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Номер соглашения
    /// </summary>
    [MaxLength(15)] public string AgreementNumber { get; set; } = null!;
    /// <summary>
    /// Дата начала
    /// </summary>
    public DateTime StarDateTime { get; set; }
    /// <summary>
    /// Дата окончания
    /// </summary>
    public DateTime EndDateTime { get; set; }
    /// <summary>
    /// Навигационное свойство с сущностью "Подразделение в соглашении"
    /// </summary>
    [MinLength(1)] public List<DivisionInAgreement> DivisionInAgreements { get; set; } = [];
    /// <summary>
    /// Навигационное свойство с сущностью "Партнер в соглашении"
    /// </summary>
    [MinLength(1)] public List<PartnerInAgreement> PartnerInAgreements { get; set; } = [];
    /// <summary>
    /// Навигационное свойство с сущностью "Тип соглашения"
    /// </summary>
    public AgreementType AgreementType { get; set; } = null!;
    /// <summary>
    /// Навигационное свойство с сущностью "Статус соглашения"
    /// </summary>
    public AgreementStatus AgreementStatus { get; set; } = null!;

    public override string ToString()
    {
        return
            $"{AgreementNumber} {AgreementType} {StarDateTime.ToShortDateString()} - {EndDateTime.ToShortDateString()}";
    }
}