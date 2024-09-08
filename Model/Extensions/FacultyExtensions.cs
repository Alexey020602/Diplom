using Model.Divisions;

namespace Model.Extensions;

public static class FacultyExtensions
{
    public static Faculty ToModel(this DataBase.Models.Faculty faculty)
    {
        return new Faculty()
        {
            Id = faculty.Id,
            Name = faculty.Name
        };
    }

    public static DataBase.Models.Faculty ToDao(this Faculty faculty)
    {
        return new DataBase.Models.Faculty()
        {
            Id = faculty.Id,
            Name = faculty.Name
        };
    }
}