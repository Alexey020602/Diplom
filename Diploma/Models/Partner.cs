namespace Diploma.Models;

/// <summary>
/// Класс модели для сущности Патрнер
/// </summary>
public class Partner
{
    /// <summary>
    /// Идентификатор партнера
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Полное  название партнера
    /// </summary>
    public string FullName { get; set; }
    
    /// <summary>
    /// Краткое название партнера
    /// </summary>
    public string ShortName { get; set; }
    
    /// <summary>
    /// Идентификатор типа партнера
    /// </summary>
    //public int PartnerTypeId { get; set; }
    
    /// <summary>
    /// Адрес партнера
    /// </summary>
    public string Address { get; set; }
    
    /// <summary>
    /// Адрес сайта партнера
    /// </summary>
    public string Site { get; set; }
    
    /// <summary>
    /// Контактные данные пратнера
    /// </summary>
    public string ContactData { get; set; }
    
    /// <summary>
    /// Город партнера
    /// </summary>
    public string City { get; set; }
}

/// <summary>
/// Класс модели для сущности "Тип партнера"
/// </summary>
public class PartnerType
{
    /// <summary>
    /// Идентификатор типа партнера
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Название типа партнера
    /// </summary>
    public string PartnerTypeName { get; set; }
}