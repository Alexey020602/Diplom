using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
namespace DataBase.Models;

/// <summary>
/// Класс модели для сущности Патрнер
/// </summary>
[Index("ShortName", IsUnique = true)]
public class Partner
{
    /// <summary>
    /// Идентификатор партнера
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Полное  название партнера
    /// </summary>
    [MaxLength(200)]
    public string FullName { get; set; }
    
    /// <summary>
    /// Краткое название партнера
    /// </summary>
    
    [MaxLength(50)]
    //[Column(TypeName = "char")]
    public string ShortName { get; set; }
    
    /// <summary>
    /// Идентификатор типа партнера
    /// </summary>
    //public int PartnerTypeId { get; set; }
    
    /// <summary>
    /// Адрес партнера
    /// </summary>
    
    [MaxLength(100)]
    public string? Address { get; set; }
    
    /// <summary>
    /// Адрес сайта партнера
    /// </summary>
    [MaxLength(100)]
    public string? Site { get; set; }
    
    /// <summary>
    /// Контактные данные пратнера
    /// </summary>
    [MaxLength(500)]
    public string? ContactData { get; set; }
    
    /// <summary>
    /// Город партнера
    /// </summary>
    [MaxLength(100)]
    public string? City { get; set; }
    
    /// <summary>
    /// Тип партнера
    /// </summary>
    public PartnerType PartnerType { get; set; }
    
    public ICollection<PartnerInAgreement> PartnersInAgreement { get; set; }
    
    public ICollection<Interaction> Interactions { get; set; }
    
    public ICollection<Direction> Directions { get; set; }

    public override string ToString()
    {
        return $"{FullName} {PartnerType.ToString()}";
    }
}

/// <summary>
/// Класс модели для сущности "Тип партнера"
/// </summary>
[Index("Name", IsUnique = true)]
public class PartnerType
{
    /// <summary>
    /// Идентификатор типа партнера
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Название типа партнера
    /// </summary>
    [MaxLength(50)] 
    public string Name { get; set; }
    
    ///<summary>
    ///Список партнеров соответсвующего типа
    /// </summary>
    [JsonIgnore]
    public ICollection<Partner> Partners { get; set; }

    public override string ToString()
    {
        return $"{Id} {Name}";
    }
}