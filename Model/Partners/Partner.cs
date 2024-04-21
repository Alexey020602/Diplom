using System.ComponentModel.DataAnnotations;

namespace Model.Partners;

public class Partner
{
    public int Id { get; set; }
    [StringLength(200, MinimumLength = 1, ErrorMessage = "Необходимо ввести полное название партнера")] 
    public string FullName { get; set; } = string.Empty;
    [StringLength(50, MinimumLength = 1, ErrorMessage = "Необходимо ввести краткое название партнера")]
    public string ShortName { get; set; } = string.Empty;
    [StringLength(100)] public string Address { get; set; } = string.Empty;
    [StringLength(100)] public string Site { get; set; } = string.Empty;
    [StringLength(500)] public string ContactData { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public Type? Type { get; set; }
    [MinLength(1)] public List<Direction> Directions { get; set; } = [];
    public List<Agreement> Agreements { get; set; } = [];
    public List<Interaction> Interactions { get; set; } = [];
    public bool CanBeDeleted => !(AgreementsContained || InteractionsContained);
    private bool AgreementsContained => Agreements.Any();
    private bool InteractionsContained => Interactions.Any();
}