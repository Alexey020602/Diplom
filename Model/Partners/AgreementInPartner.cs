namespace Model.Partners;

public class AgreementInPartner
{
    public int Id { get; init; }
    public string Description { get; set; } = string.Empty;
    public string ContactPerson { get; set; } = string.Empty;

    public override string ToString()
    {
        return $"""
                {Description}
                {ContactPerson}
                """;
    }
}