using Model.Divisions;
using Model.Extensions;

namespace Model.Mappers;

public static class DivisionExtensions
{
    public static Division ToModel(this DataBase.Models.Division division)
    {
        return new Division()
        {
            Id = division.Id,
            ShortName = division.ShortName,
            FullName = division.FullName,
            Faculty = division.Faculty.ToModel(),
            Contacts = division.Contacts,
            Site = division.Site,
            Directions = division.Directions.Select(DirectionExtensions.ConvertToModel).ToList()
        };
    }

    public static DivisionDetail ToDivisionDetail(this DataBase.Models.Division division) => new()
    {
        Id = division.Id,
        ShortName = division.ShortName,
        FullName = division.FullName,
        Faculty = division.Faculty.ToModel(),
        Contacts = division.Contacts,
        Site = division.Site,
        Directions = division.Directions.Select(DirectionExtensions.ConvertToModel).ToList(),
        CanDelete = division.Interactions.Count == 0 && division.DivisionsInAgreement.Count == 0
    };

    public static DataBase.Models.Division ToDao(this Division division)
    {
        return new DataBase.Models.Division()
        {
            Id = division.Id,
            ShortName = division.ShortName,
            FullName = division.FullName,
            Faculty = division.Faculty!.ToDao(),
            Contacts = division.Contacts,
            Site = division.Site,
            Directions = division.Directions.Select(DirectionExtensions.ConvertToDao).ToList()
        };
    }

    public static DivisionShort ConvertToDivisionShort(this DataBase.Models.Division division)
    {
        return new DivisionShort(division.Id, division.ShortName);
    }
    
    public static DataBase.Models.Division GetDivisionFormDivisionShort(this DivisionShort division) => new ()
        {
            Id = division.Id
        };
}