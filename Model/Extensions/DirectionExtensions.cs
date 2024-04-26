namespace Model.Extensions;

public static class DirectionExtensions
{
    public static DataBase.Models.Direction ConvertToDao(this Model.Direction direction) => new()
    {
        Id = direction.Id,
        Name = direction.Name,
    };

    public static Model.Direction ConvertToModel(this DataBase.Models.Direction direction) => new()
    {
        Id = direction.Id,
        Name = direction.Name,
    };
}