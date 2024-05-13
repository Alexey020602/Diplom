using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
namespace DataBase.Models;

/// <summary>
/// Класс модели для сущности Патрнер
/// </summary>
[Index(nameof(ShortName), IsUnique = true)]
public class Partner
{
    /// <summary>
    /// Идентификатор партнера
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Полное  название партнера
    /// </summary>
    [StringLength(200)]
    public string FullName { get; set; } = string.Empty;
    /// <summary>
    /// Краткое название партнера
    /// </summary>
    [StringLength(50)]
    public string ShortName { get; set; } = string.Empty;
    /// <summary>
    /// Адрес партнера
    /// </summary>
    [StringLength(100)]
    public string? Address { get; set; } 
    /// <summary>
    /// Адрес сайта партнера
    /// </summary>
    [StringLength(100)]
    public string? Site { get; set; }
    /// <summary>
    /// Контактные данные пратнера
    /// </summary>
    [StringLength(500)]
    public string? ContactData { get; set; }
    /// <summary>
    /// Город партнера
    /// </summary>
    [StringLength(100)]
    public string? City { get; set; }
    //public int PartnerTypeId { get; set; }
    /// <summary>
    /// Тип партнера
    /// </summary>
    public PartnerType PartnerType { get; set; } = null!;
    [JsonIgnore]
    public IEnumerable<PartnerInAgreement> PartnersInAgreement { get; set; } = [];
    [JsonIgnore]
    public IEnumerable<Interaction> Interactions { get; set; } = [];
    [MinLength(1)] public List<Direction> Directions { get; set; } = [];

    public override string ToString() =>
        $"""
        Короткое имя: {ShortName}
        Полное имя: {FullName}
        Адрес: {Address}
        Сайт: {Site}
        Контактные данные: {ContactData}
        Город: {City}
        Тип партнера: {PartnerType} 
        Направления:{Directions}
        """;

    public bool Contains(string shortNameSubstring)
    {
        if (string.IsNullOrEmpty(shortNameSubstring)) return true; 
        
        return ShortName.Contains(shortNameSubstring);
    }

    public bool HasDirection(int directionId) => Directions.Any(direction => direction.Id == directionId);

    public bool HasType(int partnerTypeId) => PartnerType?.Id == partnerTypeId;
}

/// <summary>
/// Класс модели для сущности "Тип партнера"
/// </summary>
[Index(nameof(Name), IsUnique = true)]
public class PartnerType
{
    /// <summary>
    /// Идентификатор типа партнера
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Название типа партнера
    /// </summary>
    [StringLength(50)] public string Name { get; set; } = string.Empty;

    ///<summary>
    ///Список партнеров соответсвующего типа
    /// </summary>
    [JsonIgnore]
    public ICollection<Partner> Partners { get; set; } = [];

    public override string ToString() => Name;
    
}