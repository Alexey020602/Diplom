namespace Model.Agreements;

public record AgreementShort(
    int Id,
    string Description
)
{
    public override string ToString() => Description;
    // {
    //     return
    //         $"Номер: {Number}, Тип: {AgreementType}, Статус: {Status}. {Start.ToShortDateString()} - {End.ToShortDateString()}";
    // }
}