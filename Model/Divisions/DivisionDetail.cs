namespace Model.Divisions;

public class DivisionDetail: Division, IDeletable
{
    public bool CanDelete { get; set; }
}