namespace Model.Extensions;

public static class DirectionExtensions
{
    public static DataBase.Models.Direction ConvertToDao(this Direction direction)
    {
        return new DataBase.Models.Direction
        {
            Id = direction.Id,
            Name = direction.Name
        };
    }

    public static Direction ConvertToModel(this DataBase.Models.Direction direction)
    {
        return new Direction
        {
            Id = direction.Id,
            Name = direction.Name
        };
    }
}