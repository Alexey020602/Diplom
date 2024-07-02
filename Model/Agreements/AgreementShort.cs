namespace Model.Agreements;

public record AgreementShort(
    int Id, 
    string Number,
    Type Type,
    Status Status,
    DateTime Start,
    DateTime End
    )
{
    public override string ToString() => $"Номер: {Number}, Тип: {Type}, Статус: {Status}. {Start.ToShortDateString()} - {End.ToShortDateString()}";
}
