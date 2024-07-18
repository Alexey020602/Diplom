namespace Model.Agreements;

public record AgreementShort(
    int Id, 
    string Number,
    AgreementType AgreementType,
    Status Status,
    DateTime Start,
    DateTime End
    )
{
    public override string ToString() => $"Номер: {Number}, Тип: {AgreementType}, Статус: {Status}. {Start.ToShortDateString()} - {End.ToShortDateString()}";
}
