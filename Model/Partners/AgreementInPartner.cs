namespace Model.Partners;

public class AgreementInPartner
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public string ContactPerson { get; set; } = string.Empty;

    public override string ToString() =>
        $"""
        {Description}
        {ContactPerson}
        """;
}