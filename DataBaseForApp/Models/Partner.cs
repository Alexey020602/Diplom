using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using DataBase.Extensions;
using Microsoft.EntityFrameworkCore;

namespace DataBase.Models;

/// <summary>
///     Класс модели для сущности Патрнер
/// </summary>
[Index(nameof(ShortName), IsUnique = true)]
public class Partner
{
    /// <summary>
    ///     Идентификатор партнера
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    ///     Полное  название партнера
    /// </summary>
    [StringLength(200)] public string FullName { get; set; } = string.Empty;
    /// <summary>
    ///     Краткое название партнера
    /// </summary>
    [StringLength(50)] public string ShortName { get; set; } = string.Empty;
    /// <summary>
    ///     Адрес партнера
    /// </summary>
    [StringLength(100)] public string? Address { get; set; }
    /// <summary>
    ///     Адрес сайта партнера
    /// </summary>
    [StringLength(100)] public string? Site { get; set; }
    /// <summary>
    ///     Контактные данные пратнера
    /// </summary>
    [StringLength(500)] public string? ContactData { get; set; }
    /// <summary>
    ///     Город партнера
    /// </summary>
    [StringLength(100)] public string? City { get; set; }
    /// <summary>
    ///  Навигационное свойство с сущностью "Тип партнера"
    /// </summary>
    public PartnerType? PartnerType { get; set; }
    ///<summary>
    ///Id типа партнера
    /// </summary>
    public int PartnerTypeId { get; set; }
    /// <summary>
    /// Навигационное свойство с сущностью "Партнер в соглашении"
    /// </summary>
    public List<PartnerInAgreement> PartnersInAgreement { get; set; } = [];
    /// <summary>
    /// Навигационное свойство с сущностью "Взаимодействие"
    /// </summary>
    public List<Interaction> Interactions { get; set; } = [];
    /// <summary>
    /// Навигационное свойство с сущностью "Направление"
    /// </summary>
    [MinLength(1)] public List<Direction> Directions { get; set; } = [];

    public override string ToString()
    {
        return $"""
                Короткое имя: {ShortName}
                Полное имя: {FullName}
                Адрес: {Address}
                Сайт: {Site}
                Контактные данные: {ContactData}
                Город: {City}
                Тип партнера: {PartnerType}
                Направления:{Directions}
                """;
    }
    public static Partner Default(int number) => new()
    {
        Id = number,
        ShortName = $"Партнер {number}",
        FullName = $"Партнер {number.Name()}",
        Site = $"https://site{number}.ru",
        PartnerTypeId = number.GetId(4),
        Address = $"Адрес {number}",
    };
}