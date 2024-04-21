using DataBase.Models;

namespace Diploma.Extensions.ModelToDao;

public static class DirectionExtensions
{
    public static Direction ConvertToDao(this Model.Direction direction) => new()
    {
        Id = direction.Id,
        Name = direction.Name,
    };
}