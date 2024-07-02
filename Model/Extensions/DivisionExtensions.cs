using Model.Divisions;

namespace Model.Extensions;

public static class DivisionExtensions
{
    public static Model.Divisions.Division ToModel(this DataBase.Models.Division division) => new Division()
    {
        Id = division.Id,
        ShortName = division.ShortName,
        FullName = division.FullName,
        Faculty = division.Faculty.ToModel(),
        Contacts = division.Contacts,
        Site = division.Site,
        Directions = division.Directions.Select(DirectionExtensions.ConvertToModel).ToList()
    };

    public static DataBase.Models.Division ToDao(this Model.Divisions.Division division) =>
        new DataBase.Models.Division()
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