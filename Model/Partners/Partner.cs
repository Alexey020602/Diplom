using System.ComponentModel.DataAnnotations;

namespace Model.Partners;

public class Partner: IEquatable<Partner>
{
    public int Id { get; init; }

    [StringLength(200, MinimumLength = 1, ErrorMessage = "Необходимо ввести полное название партнера")]
    public string FullName { get; set; } = string.Empty;

    [StringLength(50, MinimumLength = 1, ErrorMessage = "Необходимо ввести краткое название партнера")]
    public string ShortName { get; set; } = string.Empty;

    [StringLength(100)] public string Address { get; set; } = string.Empty;
    [StringLength(100)] public string Site { get; set; } = string.Empty;
    [StringLength(500)] public string ContactData { get; set; } = string.Empty;
    [StringLength(100)] public string City { get; set; } = string.Empty;
    [Required] public PartnerType? Type { get; set; }

    [MinLength(1, ErrorMessage = "Необходимо указать как минимум одно направление деятельности партнера")]
    public List<Direction> Directions { get; set; } = [];

    public List<AgreementInRelationship> Agreements { get; set; } = [];
    public List<InteractionInRelationship> Interactions { get; set; } = [];
    public bool CanBeDeleted => !(AgreementsContained || InteractionsContained);
    private bool AgreementsContained => Agreements.Any();
    private bool InteractionsContained => Interactions.Any();

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public override bool Equals(object? obj) => obj is Partner other && Equals(other);
    public bool Equals(Partner? other)
    {
        return other != null && other.Id == Id;
    }
}