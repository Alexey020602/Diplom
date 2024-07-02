using Model.Divisions;

namespace Model.Extensions;

public static class FacultyExtensions
{
    public static Faculty ToModel(this DataBase.Models.Faculty faculty) => new Faculty()
    {
        Id = faculty.Id,
        Name = faculty.Name
    };

    public static DataBase.Models.Faculty ToDao(this Faculty faculty) => new DataBase.Models.Faculty()
    {
        Id = faculty.Id,
        Name = faculty.Name
    };
}