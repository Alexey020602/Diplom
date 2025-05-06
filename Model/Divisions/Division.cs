using System.ComponentModel.DataAnnotations;

namespace Model.Divisions;

public class Division: IEquatable<Division>
{
    public int Id { get; init; }

    [StringLength(200, ErrorMessage = "Краткое название не может содержать больше 200 символов")]
    [Required(ErrorMessage = "Необходимо ввести краткое название подразделения")]
    public string ShortName { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "Полное название не может содержать больше 500 символов")]
    [Required(ErrorMessage = "Необходимо ввести полное название подразделения")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Необходимо выбрать факультет")]
    public Faculty? Faculty { get; set; }

    [StringLength(500)] public string? Contacts { get; set; }
    [StringLength(100)] public string? Site { get; set; }

    [MinLength(1, ErrorMessage = "Необходимо выбрать хотя бы одно направление")]
    public List<Direction> Directions { get; set; } = [];

    public List<AgreementInRelationship> Agreements { get; set; } = [];
    public List<InteractionInRelationship> Interactions { get; set; } = [];
    
    public bool CanBeDeleted => !(Agreements.Any() || Interactions.Any());

    public override bool Equals(object? obj)
    {
        return obj is Division other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public bool Equals(Division? other)
    {
        return other != null && other.Id == Id;
    }
}