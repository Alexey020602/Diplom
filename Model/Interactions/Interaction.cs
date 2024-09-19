using System.ComponentModel.DataAnnotations;
using Model.Divisions;
using Model.Partners;

namespace Model.Interactions;

public class Interaction
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Необходимо выбрать пратнера, участвующего во взаимодействии")]
    public PartnerShort? Partner { get; set; }

    [Required(ErrorMessage = "Необходимо выбрать подразделение, участвующее во взаимодействии")]
    public DivisionShort? Division { get; set; }

    [Required(ErrorMessage = "Необходимо выбрать тип взаимодействия")]
    public InteractionType? Type { get; set; }

    [StringLength(500, ErrorMessage = "Тема не может превышать 500 символов")]
    [Required(ErrorMessage = "Необходимо ввести тему взаимодействия")]
    public string Theme { get; set; } = string.Empty;

    [Required(ErrorMessage = "Необходимо ввести код взаимодействия")]
    [StringLength(9, ErrorMessage = "Код не может быть больше 9 символов")]
    public string ContactCode { get; set; } = string.Empty;

    public DateOnly SigningDate { get; set; }
    public DateOnly Begin { get; set; }
    public DateOnly End { get; set; }
    [MinLength(1, ErrorMessage = "Необходимо выбрать хотя бы одно направление")] public List<Direction> Directions { get; set; } = [];
    public override string ToString()
    {
        return $"{ContactCode} типа {Type} от {SigningDate.ToShortDateString()}, {Begin.ToShortDateString()} - {End.ToShortDateString()}";
    }
}